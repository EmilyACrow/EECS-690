using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speedModifier = 7.0f;
    [SerializeField] private float _mouseVertSensitivity = 70.0f;
    [SerializeField] private float _mouseHorzSensitivity = 15.0f;
    [SerializeField] private float _minXRotation = -70.0f;
    [SerializeField] private float _maxXRotation = 80.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _jumpHeight = 2.0f;

    //Struct for storing player inputs from update loop
    struct PlayerInput {
        public float horizontal;
        public float vertical;
        public bool jump;

        public PlayerInput(float h, float v, bool j) {
            this.horizontal = h;
            this.vertical = v;
            this.jump = j;
        }

    }

    private Vector3 _playerVelocity;
    private float _xRotation = 0.0f;
    private CharacterController _characterController;
    private Camera _camera;
    private float _jumpVelocity;
    private PlayerInput _input;
    private float _groundStickyVelocity = -1.0f;
    private bool amIGrounded;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _jumpVelocity = Mathf.Sqrt(_jumpHeight * _gravity * -2);
        _input = new PlayerInput(0.0f,0.0f,false);

        if (_characterController == null) {
            Debug.Log("No character controller attached to player.\n");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    // Player inputs are caught here and handled in FixedUpdate()
    void Update()
    {
        amIGrounded = _characterController.isGrounded;
        GetPlayerInput();
        PlayerLook();
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    private void GetPlayerInput() {
        // WASD or joystick
        _input.horizontal = Input.GetAxis("Horizontal");
        _input.vertical = Input.GetAxis("Vertical");

        //Spacebar or joystick axis 3
        if(!_input.jump && Input.GetButtonDown("Jump")) {
            _input.jump = true;
        }
    }

    private void PlayerMovement() {
        //COnvert captured inputs into movement vector
        Vector3 movement = transform.right * _input.horizontal + transform.forward * _input.vertical;
        //Normalize input vector to prevent diagonal movement from being faster
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        //Multiply speed vector by speed modifier so that it doesn't affect gravity
        movement *=  _speedModifier;

        //Set player's horizontal velocity to inputs
        _playerVelocity.x = movement.x;
        _playerVelocity.z = movement.z;
        
        //If the character is on the ground, apply a small downward force
        if(_characterController.isGrounded) {
            _playerVelocity.y = _groundStickyVelocity;
        } else {
            //Apply a larger gravitational force if not grounded
            _playerVelocity.y += _gravity * Time.deltaTime;
        }

        //Check if player is trying to jump
        if(_input.jump) {
            Debug.Log("Jumping:" + _jumpVelocity);
            PlayerJump();
            _input.jump = false;
        }

        //Move player
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void PlayerLook() {
        //Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * _mouseVertSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseVertSensitivity * Time.deltaTime;

        //Limit vertical look
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minXRotation, _maxXRotation);

        //Move camera horizontally
        _camera.transform.localRotation = Quaternion.Euler(_xRotation,0,0);
        //Rotate player
        transform.Rotate(Vector3.up * mouseX * _mouseHorzSensitivity * Time.deltaTime); 
    }

    //If the player can jump, add the jump velocity to the player controller
    private void PlayerJump() {
        if(_characterController.isGrounded) {
            _playerVelocity.y += _jumpVelocity;
        }
    }

}
