using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Player moves in relation to the camera's rotation.
// Uses Unity's preview input system.
public class PlayerMovement : MonoBehaviour
{
    // Variables.
    public static Vector3 playerPos;

    [SerializeField]
    private float playerSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float x;
    [SerializeField]
    private float z;

    [SerializeField]
    private Vector3 move;

    [SerializeField]
    private Vector2 rotY;

    private Rigidbody rb;

    public static PlayerControls controls;

    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    // Pivot is an empty gameobject that shares the position of the
    // camera, but always faces forward. It's rotation is not
    // changed.
    private GameObject pivot;

    // Mouse controls.
    public bool isUsingMouse;
    public float adjustmentAngle;

    [SerializeField]
    private AudioSource playerAudio;

    Animator playerAnimator;
    
    void Awake()
    {
        controls = new PlayerControls();
        rb = gameObject.GetComponent<Rigidbody>();

        // Controller input.
        controls.Gameplay.PlayerMoveX.performed += ctx => x 
        = ctx.ReadValue<float>();
        controls.Gameplay.PlayerMoveX.canceled += ctx => x = 0f;

        controls.Gameplay.PlayerMoveZ.performed += ctx => z 
        = ctx.ReadValue<float>();
        controls.Gameplay.PlayerMoveZ.canceled += ctx => z = 0f;


        controls.Gameplay.PlayerRotY.performed += ctx => rotY
        = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerRotY.canceled += ctx => rotY = Vector2.zero;

        playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        playerPos = transform.position;

        pivot.transform.position = mainCamera.transform.position;

        var euler = mainCamera.transform.rotation.eulerAngles;
        var rot = Quaternion.Euler(0, euler.y, 0);
        pivot.transform.rotation = rot;

        // Player moves on the Z axis based on the camera's rotation.
        move = (mainCamera.transform.right * x + pivot.transform.forward * z).normalized;

        // If there's not input from the input system, 
        // check for alternative input.
        if (move == Vector3.zero) 
        {
            move = KeyboardMovement(move);
        }

        if (rotY != Vector2.zero)
        {
            FaceTarget(rotY);
        }
        else
        {
            // Used for debugging in the editor.
            // It is recommended that this is set to FALSE in builds.
            if (isUsingMouse)
            {
                FaceMouse();
            }
        }
    }

    // Physics.
    void FixedUpdate()
    {
        if (move != Vector3.zero && !playerAudio.isPlaying)
        {
            playerAudio.Play();
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            playerAudio.Pause();
            
        }

        if(move == Vector3.zero)
        {
            playerAnimator.SetBool("Walking", false);
        }

        // Move the player.
        rb.position += (move * playerSpeed * Time.deltaTime);
    }

    // Required for the input system.
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Point towards the direction of the right analogue stick.
    void FaceTarget(Vector2 rot)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-rot.x, 0f, -rot.y)).normalized;

        transform.rotation = Quaternion.Slerp(transform.rotation,
                     lookRotation, Time.deltaTime * rotationSpeed).normalized;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    // Temporary alternative movement.
    // Used for debugging.
    Vector3 KeyboardMovement(Vector3 move)
    {
        float keyboardX = Input.GetAxis("Horizontal");
        float keyboardZ = Input.GetAxis("Vertical");

        move = mainCamera.transform.right * keyboardX + pivot.transform.forward
            * keyboardZ;

        return move;
    }

    void FaceMouse()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + adjustmentAngle;

        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }
}