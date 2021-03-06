using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// One LevelManager object in the scene, containing this script.
// This Controls spawning of rooms from prefabs to create a level.
public class LevelGeneration : MonoBehaviour
{
	// Varaibles.
	public GameObject[] roomPrefabs;
	[SerializeField] public List<GameObject> roomsInScene = new List<GameObject>();
	public int maximumRooms;
	[SerializeField] private int totalRoomsSoFar = 1;
	[SerializeField] private int openPaths;
	public GameObject startRoomPrefab;
	public List<GameObject> openSpawnPts = new List<GameObject>();
	Quaternion startRot;
	Vector3 startSpawn;

	public List<GameObject> rooms_northDoor = new List<GameObject>();
	public List<GameObject> rooms_eastDoor = new List<GameObject>();
	public List<GameObject> rooms_southDoor = new List<GameObject>();
	public List<GameObject> rooms_westDoor = new List<GameObject>();

	public GameObject deadEndN;
	public GameObject deadEndE;
	public GameObject deadEndS;
	public GameObject deadEndW;
	public GameObject all4;

	public EnemyCountManager enemyCountMan;
	public bool startedEnemyCounter = false;
	private bool hasStoppedSpawning = false;

	public CameraHideAllWalls cameraHideAllWalls;

	// Padlock Management
	public Room currentRoom;
	public Animator padlockAnimator;

	public bool startRoomGen = false;

	public static bool isFinished = false;

	private void Start()
	{
		// To start, we just create the starter room at 0.0.
		startSpawn = new Vector3(0, 0, 0);
		startRot = new Quaternion(0, 0, 0, 0);
		GameObject newRoom = Instantiate(startRoomPrefab, startSpawn, startRot);
		populateRoomDirectionLists();
		openPaths = openSpawnPts.Count;

		roomsInScene.Add(newRoom);

		// BUILD LEVEL.
		InvokeRepeating("spawnNewRoom", 0.5f, 0.1f);
	}

	private void FixedUpdate()
	{
		// If there's no more free spawn points and room generation is finished rather than just started, stop making new rooms.
		if (openSpawnPts.Count() == 0 && startRoomGen)
		{
			CancelInvoke("spawnNewRoom");
			hasStoppedSpawning = true;

			if (!startedEnemyCounter)
			{
				// Now whole level has been built we can count all the enemies, find all of the hideable objects.
				cameraHideAllWalls.populateHideableList();
				enemyCountMan.startUpEnemyCounter();
				startedEnemyCounter = true;

				isFinished = true;
			}
		}

		if (openSpawnPts.Count() != 0 && hasStoppedSpawning)
		{
			InvokeRepeating("spawnNewRoom", 0.5f, 0.1f);
		}

		updatePadlockAnimator();
	}

	public void spawnNewRoom()
	{
		// Checks there aren't more open spawn points, get spawn point from list, and decide how to spawn a room.
		if (openSpawnPts.Count > 0)
		{
			startRoomGen = true;
			int index = Random.Range(0, openSpawnPts.Count - 1);
			if (openSpawnPts[index].GetComponent<RoomSpawnPoint>().open)
			{
				pickHowToSpawnRoom(openSpawnPts[index].GetComponent<RoomSpawnPoint>());
			}
			else
			{
				openSpawnPts.Remove(openSpawnPts[index].GetComponent<RoomSpawnPoint>().gameObject);
			}
		}
		
	}
	

	public List<GameObject> getRoomsInScene()
	{
		return roomsInScene;
	}

	// Managing list of active spawn points
	#region
	
	public void addNewSpawnPt(GameObject g)
	{
		// when a new room spawn pt is instatiated, it should call this to add itself to the active list
		if (g.GetComponent<RoomSpawnPoint>().open == true)
		{
			openSpawnPts.Add(g);
			openPaths++;
		}
	}



	public void removeFromSpawnList(GameObject g)
	{
		if (openSpawnPts.Contains(g))
		{
			// when a room is spawned, the relevant point becomes inactive, and this is called to remove it
			openSpawnPts.Remove(g);
			openPaths--;
			g.GetComponent<RoomSpawnPoint>().setSpawnInactive();
			
		}
	}



	#endregion

	// Manage roomsInScene

	#region
	public void addNewRoomToScene(GameObject g, RoomSpawnPoint spawn)
	{
		// adds Gamoebject to roomsInScene list
		roomsInScene.Add(g);
		totalRoomsSoFar++;
		spawn.setSpawnInactive();
		removeFromSpawnList(spawn.gameObject);
	}

	public void removeRoomFromScene(GameObject g)
	{
		// adds Gameobject to roomsInScene list
		roomsInScene.Remove(g);
		totalRoomsSoFar--;
	}
	#endregion
	// Populate lists of room prefabs based on door directions
	private void populateRoomDirectionLists()
	{
		// Populate the lists with every room that has a door in a given direction.
		foreach(GameObject g in roomPrefabs)
		{
			Room tempRoom = g.GetComponent<Room>();

			if(tempRoom.nDoor)
			{
				rooms_northDoor.Add(g);
			}
			if(tempRoom.eDoor)
			{
				rooms_eastDoor.Add(g);
			}
			if(tempRoom.sDoor)
			{
				rooms_southDoor.Add(g);
			}
			if(tempRoom.wDoor)
			{
				rooms_westDoor.Add(g);
			}
		}
	}

	// Spawning of rooms.
	public void pickHowToSpawnRoom(RoomSpawnPoint spawnPoint)
	{
		// Selects whether room should be selected from list or be a dead end.

		List<string> doorsRequired = new List<string>();
		List<string> doorsAvoided = new List<string>();

		string direction = spawnPoint.spawnDirection;


		// Check sensors to see if any doors are needed.
		foreach (RoomSpawnSensor sensor in spawnPoint.GetComponentsInChildren<RoomSpawnSensor>())
		{

			if (sensor.checkRoomWasFound() && sensor.checkMustHave())
			{
				doorsRequired.Add(sensor.getRequiredDoorDir());
			}
			if (sensor.checkRoomWasFound() && sensor.checkMustNot())
			{
				doorsAvoided.Add(sensor.getRequiredDoorDir());
			}
		}

		if (totalRoomsSoFar + openPaths >= maximumRooms)
		{
			spawnDeadEnd(spawnPoint, doorsRequired, doorsAvoided);
		}
		else if(totalRoomsSoFar + openPaths < maximumRooms)
		{
			spawnRoomFromList(spawnPoint, doorsRequired, doorsAvoided);
		}
	}

	// Spawns in a random room at the given point.
	// Room must be selected from correct list so doors align.
	public void spawnRoomFromList(RoomSpawnPoint spawnPoint, List<string> requiredDirs, List<string> avoidedDirs)
	{
		GameObject roomToSpawn = null;

		List<GameObject> roomsToChooseFrom = populateTempRoomList(requiredDirs, avoidedDirs);
		
		int index = Random.Range(0, roomsToChooseFrom.Count - 1);
		
		roomToSpawn = roomsToChooseFrom[index];

		instantiateRoom(roomToSpawn, spawnPoint);
	}

	public void spawnDeadEnd(RoomSpawnPoint spawnPoint, List<string> requiredDirs, List<string> avoidedDirs)
	{
		if (requiredDirs.Count() != 0)
		{
			GameObject roomToSpawn = findDeadEnd(requiredDirs, avoidedDirs);

			instantiateRoom(roomToSpawn, spawnPoint);
		}
	}

	// Instantiates room at spawn position, and adds to roomList.
	public void instantiateRoom(GameObject room, RoomSpawnPoint spawn)
	{
		if (room && spawn)
		{
			Vector3 tempTransform = spawn.transform.position;

			// Spawn said room at that pos.
			GameObject newRoom = Instantiate(room, tempTransform, startRot);

			addNewRoomToScene(newRoom.gameObject, spawn);
		}
	}

	// Returns a list of room prefabs that have doors in required directions and no doors in avoided directions. 
	public List<GameObject> populateTempRoomList(List<string> requiredDirs, List<string> avoidedDirs)
	{
		List<GameObject> tempRoomList = new List<GameObject>();
		List<GameObject> secondTempRoomList = new List<GameObject>();

		if(requiredDirs.Count() == 0)
		{
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().closeGame();
		}

		// Add all the doors with required directions to temp List.
		if (requiredDirs.Contains("N"))
		{
			foreach(GameObject g in rooms_northDoor)
			{
				tempRoomList.Add(g);
			}
		}
		if (requiredDirs.Contains("E"))
		{
			foreach (GameObject g in rooms_eastDoor)
			{
				tempRoomList.Add(g);
			}
		}
		if (requiredDirs.Contains("S"))
		{
			foreach (GameObject g in rooms_southDoor)
			{
				tempRoomList.Add(g);
			}
		}
		if (requiredDirs.Contains("W"))
		{
			foreach (GameObject g in rooms_westDoor)
			{
				tempRoomList.Add(g);
			}
		}

		// Remove any rooms that only have one/some of the needed directions.
		if (requiredDirs.Contains("N"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if(!rooms_northDoor.Contains(g))
				{
					secondTempRoomList.Add(g);
				}
			}
		}
		if (requiredDirs.Contains("E"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (!rooms_eastDoor.Contains(g))
				{
					secondTempRoomList.Add(g);
				}
			}
		}
		if (requiredDirs.Contains("S"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (!rooms_southDoor.Contains(g))
				{
					secondTempRoomList.Add(g);
				}
			}
		}
		if (requiredDirs.Contains("W"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (!rooms_westDoor.Contains(g))
				{
					secondTempRoomList.Add(g);
				}
			}
		}

		// Remove any rooms with doors in the mustAvoid list.
		if (avoidedDirs.Contains("N"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (g.GetComponent<Room>().nDoor)
				{
					secondTempRoomList.Add(g);
				}
			}
		}

		if (avoidedDirs.Contains("E"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (g.GetComponent<Room>().eDoor)
				{
					secondTempRoomList.Add(g);
				}
			}
		}
		if (avoidedDirs.Contains("S"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (g.GetComponent<Room>().sDoor)
				{
					secondTempRoomList.Add(g);
				}
			}
		}
		if (avoidedDirs.Contains("W"))
		{
			foreach (GameObject g in tempRoomList)
			{
				if (g.GetComponent<Room>().wDoor)
				{
					secondTempRoomList.Add(g);
				}
			}
		}

		foreach (GameObject g in secondTempRoomList)
		{
			tempRoomList.Remove(g);
		}
		
		return tempRoomList;
	}

	public GameObject findDeadEnd(List<string> requiredDirs, List<string> avoidedDirs)
	{
		// Finds appropriate room to close off a route without creating any more open paths, 
		// based on doors needed and their directions. 
		GameObject perfectRoom = null;

		// Only one door needed.
		if(requiredDirs.Count() == 1)
		{
			if(requiredDirs[0] == "N")
			{
				perfectRoom = deadEndN;
			}
			if (requiredDirs[0] == "E")
			{
				perfectRoom = deadEndE;
			}
			if (requiredDirs[0] == "S")
			{
				perfectRoom = deadEndS;
			}
			if (requiredDirs[0] == "W")
			{
				perfectRoom = deadEndW;
			}
		}

		// If two doors needed.
		if (requiredDirs.Count() == 2)
		{
			foreach (GameObject g in roomPrefabs)
			{
				Door[] doors = g.GetComponentsInChildren<Door>();

				if (doors.Count() == 2)
				{
					if (requiredDirs.Contains(doors[0].direction) && requiredDirs.Contains(doors[1].direction))
					{
						perfectRoom = g;
					}
				}
			}
		}

		// If 3 doors needed.
		if (requiredDirs.Count() == 3)
		{
			foreach (GameObject g in roomPrefabs)
			{
				Door[] doors = g.GetComponentsInChildren<Door>();

				if (doors.Count() == 3)
				{
					if (requiredDirs.Contains(doors[0].direction) && requiredDirs.Contains(doors[1].direction) && requiredDirs.Contains(doors[2].direction))
					{
						perfectRoom = g;
					}
				}
			}
		}

		// If all 4 doors needed.
		if (requiredDirs.Count() == 4)
		{
			perfectRoom = all4;
		}

		// Error handling.
		if(!perfectRoom )
		{
			Debug.Log("No dead end found. Required dirs: " + requiredDirs.Count());
			if(requiredDirs.Count() == 2)
			{
				Debug.Log("Dirs needed: " + requiredDirs[0] + requiredDirs[1]);
			}
		}

		return perfectRoom;
	}

	// Updates padlock animator in the UI to show if current room is locked or not.
	public void updatePadlockAnimator()
	{
		if (currentRoom)
		{
			// When called, it checks which room has player in and updates padlock animator to say whether it's locked or not.
			if (currentRoom.playerInRoom)
			{
				// If player is still in the given room.
				if (currentRoom.doorsLocked != padlockAnimator.GetBool("Locked"))
				{
					// If anim and rooms values for locked don't match.
					padlockAnimator.SetBool("Locked", currentRoom.doorsLocked);
				}
			}
			else
			{
				// Player has changed room, refind which room they're in.
				foreach (GameObject roomGO in roomsInScene)
				{
					if (roomGO.GetComponent<Room>().playerInRoom)
					{
						currentRoom = roomGO.GetComponent<Room>();
						padlockAnimator.SetBool("Locked", currentRoom.doorsLocked);
					}
				}
			}
		}
		else
		{
			foreach (GameObject roomGO in roomsInScene)
			{
				if (roomGO.GetComponent<Room>().playerInRoom)
				{
					currentRoom = roomGO.GetComponent<Room>();
					padlockAnimator.SetBool("Locked", currentRoom.doorsLocked);
				}
			}
		}

	}
}
