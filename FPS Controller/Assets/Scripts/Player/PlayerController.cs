using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private NetworkVariable<Vector3> networkMovement = new NetworkVariable<Vector3>();
    private NetworkVariable<float> networkRotation = new NetworkVariable<float>();

    [SerializeField] private Vector2 defaultPositionRange = new Vector2(-4, 4);
    private Vector3 defaultPosition = new Vector3(1,1,35);

    [SerializeField] private float groundSpeedModifier = 7.0f;
    [SerializeField] private float airSpeedModifier = 3.0f;
    [SerializeField] private float walkSpeed = 7.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float stickToGroundForce = -1.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingCheckDistance = 0.5f;
    [SerializeField] private LayerMask ceilingMask;

    private CharacterController characterController;
    private PlayerInput input;
    private Vector3 inputMovement = Vector3.zero;  
    private float cachedRotation; 
    private float currentRotation;
    private float jumpVelocity;

    // Awake is called before Start
    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y) + defaultPosition.x, 
                defaultPosition.y,
                Random.Range(defaultPositionRange.x, defaultPositionRange.y) + defaultPosition.z);
                   
            input = new PlayerInput(0.0f,0.0f,false);

            PlayerCameraFollow.Instance.FollowPlayer(transform.Find("CameraTransform"));

            cachedRotation = Camera.main.transform.localEulerAngles.y;

            jumpVelocity = Mathf.Sqrt(jumpHeight * gravity * -2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsClient && IsOwner) {
            UpdateClient();
        } else if (IsServer){
            UpdateServer();
        }
    }

    private void UpdateClient() {
        if (IsClient && IsOwner)
        {
            inputMovement = Vector3.zero;
            input.inputDetected = false;
            //Get Player input
            GetPlayerInput();
            GetCameraRotation();
            if(input.inputDetected) {
                PlayerMovementHorizontal();
            }
            if(input.inputDetected || !characterController.isGrounded) {
                PlayerMovementVertical();
            }
            //If input has changed, update client position on server
            if(inputMovement != Vector3.zero || currentRotation != cachedRotation) {
                inputMovement *=  walkSpeed * Time.deltaTime;
                cachedRotation = currentRotation;
                UpdateClientPositionRotationServerRpc(inputMovement, currentRotation);
            }
            PlayerMove();
        }
    }

    private void PlayerMovementHorizontal() {
        //COnvert captured inputs into movement vector
        Vector3 movement = transform.right * input.horizontal + transform.forward * input.vertical;
        //Normalize input vector to prevent diagonal movement from being faster
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        if(characterController.isGrounded) {
            //Multiply speed vector by speed modifier so that it doesn't affect gravity
            movement *= groundSpeedModifier;
        } else {
            movement *= airSpeedModifier;
        }

        //Set player's horizontal velocity to inputs
        inputMovement.x = movement.x;
        inputMovement.z = movement.z;      
    }    

    private void UpdateServer() {
        transform.position = new Vector3(transform.position.x + networkMovement.Value.x, transform.position.y,
            transform.position.z + networkMovement.Value.z);
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
            input.inputDetected = true;
            input.jump = true;
        }

        if (!input.inputDetected && (input.horizontal != 0 || input.vertical != 0)) {
            input.inputDetected = true;
        }

        if(Input.GetMouseButtonDown(1)) {
            ToggleMouse();
        }

    }

    private void GetCameraRotation() {
        //get rotation of camera
        currentRotation = Camera.main.transform.localEulerAngles.y;
        RotatePlayer(Quaternion.Euler(0,currentRotation, 0));
    }

    private void RotatePlayer(Quaternion euler) {
        transform.localRotation = euler;
    }

    private void PlayerMove() {
        if (networkMovement.Value != Vector3.zero) {
            characterController.Move(networkMovement.Value);
        }
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

    [ServerRpc]
    public void UpdateClientPositionRotationServerRpc(Vector3 pos, float rot) 
    {
        networkMovement.Value = pos;
        networkRotation.Value = rot;
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(Vector3 pos) 
    {
        networkMovement.Value = pos;
    }

    //========================================================================
    struct PlayerInput {
        public float horizontal;
        public float vertical;
        public bool jump;
        public bool inputDetected;
        public bool cursorLocked;

        public PlayerInput(float h, float v, bool j) {
            this.horizontal = h;
            this.vertical = v;
            this.jump = j;
            inputDetected = false;
            cursorLocked = false;
        }
    }

}
