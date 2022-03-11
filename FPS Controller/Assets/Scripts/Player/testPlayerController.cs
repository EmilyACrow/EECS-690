using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerController : MonoBehaviour
{
    public bool m_canMove { get; private set; } = true;

    private bool ShouldJump => Input.GetKeyDown(m_jumpKey) && m_characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool m_canJump = true;
    [SerializeField] private bool m_useFootsteps = true;

    [Header("Controls")]
    [SerializeField] private KeyCode m_jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] private float m_walkSpeed = 3.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float m_lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float m_lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float m_upperLookLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float m_lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float m_jumpForce = 8.0f;
    [SerializeField] private float m_gravity = 30.0f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingCheckDistance = 0.5f;
    [SerializeField] private LayerMask ceilingMask;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMult = 1.5f;
    [SerializeField] private float sprintStepMult = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] bareClips = default;
    private float footstepTimer = 0;
    //private float getCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMult : isSprinting ? baseStepSpeed * sprintStepMult : baseStepSpeed;
    private float getCurrentOffset => baseStepSpeed;

    private Camera m_playerCamera;
    private CharacterController m_characterController;

    private Vector3 m_moveDirection;
    private Vector2 m_currentInput;

    private float m_rotationX = 0;

    void Start(){
        m_playerCamera = GetComponentInChildren<Camera>();
        m_characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        if(m_canMove){
            HandleMovementInput();
            HandleMovementLook();

            if(m_canJump){
                HandleJump();
            }

            if(m_useFootsteps){
                Handle_Footsteps();
            }

            ApplyMovements();
        }
    }

    private void HandleMovementInput(){
        m_currentInput = new Vector2(m_walkSpeed * Input.GetAxis("Vertical"), m_walkSpeed * Input.GetAxis("Horizontal"));

        float moveDirectionY = m_moveDirection.y;
        m_moveDirection = (transform.TransformDirection(Vector3.forward) * m_currentInput.x) + (transform.TransformDirection(Vector3.right) * m_currentInput.y);
        m_moveDirection.y = moveDirectionY;
    }

    private void HandleMovementLook(){
        m_rotationX -= Input.GetAxis("Mouse Y") * m_lookSpeedY;
        m_rotationX = Mathf.Clamp(m_rotationX, -m_upperLookLimit, m_lowerLookLimit);
        m_playerCamera.transform.localRotation = Quaternion.Euler(m_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_lookSpeedX, 0);
    }

    private void HandleJump(){
        if(ShouldJump){
            m_moveDirection.y = m_jumpForce;
        }
    }

    /*
    private void CheckCeiling() {
        if(Physics.CheckSphere(ceilingCheck.position, ceilingCheckDistance, ceilingMask)) {
            //We're touching the ceiling! If we have a positive y velocity, set it to zero
            if(m_moveDirection.y > 0) {
                m_moveDirection.y = 0.0f;
            }
        }
    }*/

    private void ApplyMovements(){
        if(!m_characterController.isGrounded){
            m_moveDirection.y -= m_gravity * Time.deltaTime;
        }
        m_characterController.Move(m_moveDirection * Time.deltaTime);
    }

    private void Handle_Footsteps(){
        if(!m_characterController.isGrounded) return;
        if(m_currentInput == Vector2.zero) return; //This is probably what needs to be changed. If velocity == zero, no sound should play

        footstepTimer -= Time.deltaTime;

        if(footstepTimer <= 0){
            if(Physics.Raycast(m_playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3)){
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
