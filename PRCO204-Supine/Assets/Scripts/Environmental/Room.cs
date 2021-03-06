using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
public class Room : MonoBehaviour
{
    // Rooms have at least one door.
    // Multiple rooms make up a level.
    // Rooms have objectives to be met to clear the room.
    // When player enters room for the first time, all doors lock behind them.
    // When room is cleared doors unlock.

    // VARIABLES
    #region
    public Door[] doors;
    public bool roomCleared = false;
    public bool doorsLocked = false;
    public bool playerInRoom;
    public BoxCollider inRoomTrigger;

    // door directions
    public bool nDoor = false;
    public bool eDoor = false;
    public bool sDoor = false;
    public bool wDoor = false;

    // unlock vars

    [SerializeField] private List<EnemyHealth> enemiesInRoom = new List<EnemyHealth>();
    public EnemyCountManager enemyCountManager;

    private LevelGeneration levelGenMan;
    private PickUpWeapon playerPickUp;

    public bool justCreated = true;

    public static EnemySpawnManager allSpawnManagers;

    private NetworkRoomRelay _networkRoomRelay;
    private AudioSource audio;
    #endregion

    // Start is called before the first frame update.
    void Start()
    {
        audio = GetComponent<AudioSource>();

        if(!this.GetComponentInChildren<Door>())
        {
            Debug.LogError("No doors are attached to this room. Each room requires at least one door child");
        }

        // Add a NetworkRoomRelay to this Room
        _networkRoomRelay = gameObject.AddComponent<NetworkRoomRelay>();

        doors = this.GetComponentsInChildren<Door>();
        setUpDoorDirections();

        // Get the roomTrigger.
        inRoomTrigger = GetComponentInChildren<EnterRoomTrigger>().gameObject.GetComponent<BoxCollider>();

        
        // Get enemyCountManager.
        enemyCountManager = GameObject.FindGameObjectWithTag("EnemyCountMan").GetComponent<EnemyCountManager>();

        // Get LevelGen from parent.
        levelGenMan = GameObject.FindGameObjectWithTag("LevelGenManager").GetComponent<LevelGeneration>();

        playerPickUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpWeapon>();

        populateEnemiesInRoom();

        foreach (Door d in doors)
        {
            d.doorManager = levelGenMan.GetComponent<AudioSource>();
        }

        allSpawnManagers = GetComponentInChildren<EnemySpawnManager>();
    }

    // Update is called once per frame.
    void Update()
    {
        if(playerInRoom & !roomCleared & !doorsLocked)
        {
            lockAllDoors();
        }
        else if (playerInRoom & roomCleared & doorsLocked)
        {
            unlockAllDoors();
        }

        // If this is the start room, unlock doors when they have the sword
        if(gameObject.name.Contains("start") && doorsLocked)
        {
            if(playerPickUp.isHoldingWeapon())
            {
                unlockAllDoors();
            }
        }
    }

    // Get a list of enemies.
    public void populateEnemiesInRoom()
    {
        EnemyHealth[] tempArray = GetComponentsInChildren<EnemyHealth>();

        foreach(EnemyHealth enemy in tempArray)
        {
            enemiesInRoom.Add(enemy);
        }
    }

    // Get the enemies in this room.
    public void addEnemiesToRoom(EnemyHealth newEnemy)
    {
        enemiesInRoom.Add(newEnemy);
    }
    
    // Called when an enemy is killed, removes that enemy.
    public void enemyKilled(EnemyHealth enemyKilled)
    {
        enemiesInRoom.Remove(enemyKilled);

        if(enemiesInRoom.Count == 0)
        {
            // All enemies killed.
            unlockAllDoors();
        }

        enemyCountManager.enemyKilled(enemyKilled);
    }

    public List<EnemyHealth> getEnemiesInRoom()
    {
        return enemiesInRoom;
    }

    private void setUpDoorDirections()
    {
        // Sets bools for each door dir based on the spawn pts in the children.
        foreach(Door d in doors)
        {
            if(d.direction == "N")
            {
                nDoor = true;
            }
            if (d.direction == "E")
            {
                eDoor = true;
            }
            if (d.direction == "S")
            {
                sDoor = true;
            }
            if (d.direction == "W")
            {
                wDoor = true;
            }
        }
        
    }

    public void setPlayerInRoom(bool playerInRoomInput)
    {
        playerInRoom = playerInRoomInput;
        
        if (playerInRoomInput) _networkRoomRelay.EnteredRoom();
    }

    public void lockAllDoors()
    {
        foreach(Door d in doors)
        {
            d.lockDoor();
        }

        doorsLocked = true;
        audio.Play();
        _networkRoomRelay.EnteredRoom();
    }

    public void unlockAllDoors()
    {
        foreach (Door d in doors)
        {
            d.unlockDoor();
        }

        roomCleared = true;
        doorsLocked = false;
        _networkRoomRelay.EnteredRoom();
    }

    public bool getPlayerInRoom()
    {
        return playerInRoom;
    }
    
    
    /// <summary>
    /// Gets the color used in the room, to be sent to the network.
    /// Not super heavy but probably avoid doing it each frame.
    /// Tree:
    ///   gameObject (Room)
    ///   ┗ Model
    ///     ┗ Floor
    ///       ┗ MeshRenderer
    ///         ┗ Materials
    ///           ┗ material.Color
    /// </summary>
    public Color GetColor() {
        try {
            var model = gameObject.transform.Find("Model");
            var floor = model.gameObject.transform.Find("Floor");
            var meshRenderer = floor.GetComponent<MeshRenderer>();
            var color = meshRenderer.material.color;
            return color;
        }
        catch (Exception e) {
            Debug.LogError($"Unable to traverse the tree of this Room object to get its color: {e.Message}");
            return Color.black;
        }
    }


    public class PartialDoorData {
        public bool exists; 
        public string direction;
        public bool? locked = null;
        public bool? open = null;
        public PartialDoorData(bool[] doorStatus, [CanBeNull] Door door) {
            // Get which door is attached
        
            Debug.Log(this.direction);

            // See if it exists
            
            if (door) {
                // Add more info
                this.direction = door.name.Substring(0, 1);
                this.locked = door.locked;
                this.open = door.open;
                
                if (this.direction == "N") this.exists = doorStatus[0];
                if (this.direction == "E") this.exists = doorStatus[1];
                if (this.direction == "W") this.exists = doorStatus[2];
                if (this.direction == "S") this.exists = doorStatus[3];
            }
            else {
                this.exists = false;
            }
        }
    }

    public ArrayList GetDoors() {
        ArrayList data = new ArrayList();
        
        for (int i = 0; i < 4; i++) {
            Debug.Log(i);
            Debug.Log(doors.Length);
            data.Add(new PartialDoorData(new[] {nDoor, eDoor, wDoor, sDoor}, (doors.Length -1 >= i ? doors[i] : null)));
        }
        return data;
    }
}
