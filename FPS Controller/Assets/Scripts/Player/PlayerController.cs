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
    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private bool _useFootsteps = true;
    [SerializeField] public bool _canMove = true;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMult = 1.5f;
    [SerializeField] private float sprintStepMult = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] bareClips = default;
    private float footstepTimer = 0;
    private float getCurrentOffset => baseStepSpeed;

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

    private Vector3 _playerVelocity;
    private float _xRotation = 0.0f;
    private CharacterController _characterController;
    private Camera _camera;
    private float _jumpVelocity;
    private PlayerInput _input;
    private float _groundStickyVelocity = -1.0f;
    private Vector2 _currentInput; //jake-dev
    private Vector3 _moveDirection; //jake-dev
    
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
            GetPlayerInput();
            PlayerLook();
    }

    private void FixedUpdate() {
        PlayerMovementHorizontal();
        PlayerMovementVertical();
        //Move player
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void GetPlayerInput() {
        // WASD or joystick
        _input.horizontal = Input.GetAxis("Horizontal");
        _input.vertical = Input.GetAxis("Vertical");
        _currentInput = new Vector2(_walkSpeed * Input.GetAxis("Vertical"), _walkSpeed * Input.GetAxis("Horizontal"));

        //Spacebar or joystick axis 3
        if(!_input.jump && Input.GetButtonDown("Jump")) {
            _input.jump = true;
        }
        if(_useFootsteps){
            Handle_Footsteps();
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

    private void Handle_Footsteps(){
        if(!_characterController.isGrounded) return;
        if( _currentInput == Vector2.zero) return; //This is probably what needs to be changed. If velocity == zero, no sound should play

        footstepTimer -= Time.deltaTime;

        if(footstepTimer <= 0){
            if(Physics.Raycast(_camera.transform.position, Vector3.down, out RaycastHit hit, 3)){
                switch(hit.collider.tag){
                    case "Footsteps/GRASS":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length-1)]);
                        break;
                    case "Footsteps/METAL":
                        footstepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length-1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(bareClips[Random.Range(0, bareClips.Length-1)]);
                        break;
                }
            }
            footstepTimer = getCurrentOffset;
        }
    }

}
