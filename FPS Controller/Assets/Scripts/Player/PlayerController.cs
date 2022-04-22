using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 defaultPositionRange = new Vector2(-4, 4);
    [SerializeField] private Vector3 defaultPosition = new Vector3(1,1,35);
    [SerializeField] private GameObject gun;

    [Header("Movement Parameters")]
    [SerializeField] private float groundSpeedModifier = 7.0f;
    [SerializeField] private float airSpeedModifier = 3.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float stickToGroundForce = -1.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingCheckDistance = 0.5f;
    [SerializeField] private LayerMask ceilingMask;
    [SerializeField] private bool useFootsteps = true;
    [SerializeField] private float m_sprintSpeed = 3.0f;

    [Header("Mouse Look Parameters")]
    [SerializeField] private float mouseVertSensitivity = 70.0f;
    [SerializeField] private float mouseHorzSensitivity = 15.0f;
    [SerializeField] private float minXRotation = -70.0f;
    [SerializeField] private float maxXRotation = 80.0f;

    private bool isSprinting => m_canSprint && (Input.GetKey(m_sprintKey));
    private Vector2 m_currentInput;
    [Header("Footstep Parameters")]
    [SerializeField] private bool m_canSprint = true;
    [SerializeField] private KeyCode m_sprintKey = KeyCode.LeftShift;
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float sprintStepMult = 0.3f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] bareClips = default;
    private float footstepTimer = 0;
    private float getCurrentOffset => isSprinting ? baseStepSpeed * sprintStepMult : baseStepSpeed;

    //private InventorySystem inventory;

    private float xRotation = 0.0f;
    private CharacterController characterController;
    private PlayerInput input;
    private Vector3 inputMovement = Vector3.zero;  
    private float currentRotation;
    private float jumpVelocity;
    private Camera camera;


    //Struct for storing player inputs from update loop
    struct PlayerInput {
        public float horizontal;
        public float vertical;
        public bool jump;
        public bool cursorLocked;

        public PlayerInput(float h, float v, bool j) {
            this.horizontal = h;
            this.vertical = v;
            this.jump = j;
            cursorLocked = false;
        }
    }

    // Awake is called before Start
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }
    
    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y) + defaultPosition.x, 
            defaultPosition.y,
            Random.Range(defaultPositionRange.x, defaultPositionRange.y) + defaultPosition.z);

        gun.transform.SetParent(camera.transform);
        gun.SetActive(true);

                   
        input = new PlayerInput(0.0f,0.0f,false);

        jumpVelocity = Mathf.Sqrt(jumpHeight * gravity * -2);

        // inventory = new InventorySystem();
        // UIInventory.setInventory(inventory);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.isPaused == false){
            GetPlayerInput();
            PlayerLook();
        }
    }

    private void FixedUpdate() {
        PlayerMovementHorizontal();
        PlayerMovementVertical();
        //Move player
        characterController.Move(inputMovement * Time.deltaTime);
    }

    private void PlayerMovementHorizontal() {
        //COnvert captured inputs into movement vector
        Vector3 movement = transform.right * input.horizontal + transform.forward * input.vertical;
        //Normalize input vector to prevent diagonal movement from being faster
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        if(characterController.isGrounded) {
            //Multiply speed vector by speed modifier so that it doesn't affect gravity
            if(isSprinting == true){
                movement *= groundSpeedModifier * m_sprintSpeed;
            }
            else{
                movement *= groundSpeedModifier;
            }
        } else {
            movement *= airSpeedModifier;
        }

        //Set player's horizontal velocity to inputs
        inputMovement.x = movement.x;
        inputMovement.z = movement.z;      
    }    

    private void PlayerMovementVertical() {
        if (!characterController.isGrounded) {
            inputMovement.y += gravity * Time.deltaTime;
        } else {
            inputMovement.y = stickToGroundForce;
        }

        //Check if player is trying to jump
        if(input.jump) {
            PlayerJump();
            input.jump = false;
        }

        //Check if ceiling collision
        CheckCeiling();
    }

    private void GetPlayerInput() {
        // WASD or joystick
        input.horizontal = Input.GetAxis("Horizontal");
        input.vertical = Input.GetAxis("Vertical");

        //Spacebar or joystick axis 3
        if(!input.jump && Input.GetButtonDown("Jump")) {
            input.jump = true;
        }

        if(Input.GetMouseButtonDown(1)) {
            ToggleMouse();
        }
        if(useFootsteps){
            HandleFootsteps();
        }
    }

    private void PlayerLook() {
        //Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseVertSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseVertSensitivity * Time.deltaTime;

        //Limit vertical look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        //Move camera horizontally
        camera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //Rotate player
        transform.Rotate(Vector3.up * mouseX * mouseHorzSensitivity * Time.deltaTime); 
    }

    //If the player can jump, add the jump velocity to the player controller
    private void PlayerJump() {
        if(characterController.isGrounded) {
            inputMovement.y += jumpVelocity;
        }
    }

    private void CheckCeiling() {
        if(Physics.CheckSphere(ceilingCheck.position, ceilingCheckDistance, ceilingMask)) {
            //We're touching the ceiling! If we have a positive y velocity, set it to zero
            if(inputMovement.y > 0) {
                inputMovement.y = 0.0f;
            }
        }
    }

    private void ToggleMouse() {
        if(input.cursorLocked) {
            Cursor.lockState = CursorLockMode.None;
            input.cursorLocked = false;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            input.cursorLocked = true;
        }
    }

    private void HandleFootsteps(){

        if(!characterController.isGrounded) return;
        if(inputMovement.x == 0.0f && inputMovement.z == 0.0f) return; //This is probably what needs to be changed. If velocity == zero, no sound should play

        footstepTimer -= Time.deltaTime;
        if(footstepTimer <= 0){
            if(Physics.Raycast(camera.transform.position, Vector3.down, out RaycastHit hit, 3)){
                switch(hit.collider.tag){
                    case "Footsteps/GRASS":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length-1)]);
                        break;
                    case "Footsteps/METAL":
                        footstepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length-1)]);
                        break;
                    default:
                        footstepAudioSource.volume = 1.0f; //0.0-1.0f in terms of volume
                        footstepAudioSource.PlayOneShot(bareClips[Random.Range(0, bareClips.Length-1)]);
                        break;
                }
            }
            footstepTimer = getCurrentOffset;
        }
    }


}
