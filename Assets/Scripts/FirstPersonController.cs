using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = 9.8f;
    public float mouseSensitivity = 2f;
    public float lookUpLimit = 80f;
    public float lookDownLimit = -80f;
    
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float cameraHeight = 1.6f;
    
    [Header("Advanced Settings")]
    public float airControl = 0.3f;
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2f;
    public float crouchTransitionSpeed = 10f;
    
    // Private variables
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float rotationX = 0f;
    private bool isCrouching = false;
    private float currentHeight;
    private float targetHeight;
    
    // Input System variables
    private PlayerInputActions playerInputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpInput;
    private bool runInput;
    private bool crouchInput;
    
    void Awake()
    {
        // Initialize Input System
        playerInputActions = new PlayerInputActions();
    }
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        if (playerCamera == null)
            playerCamera = Camera.main;
            
        if (playerCamera != null)
        {
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, cameraHeight, 0);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        currentHeight = standingHeight;
        targetHeight = standingHeight;
        characterController.height = currentHeight;
    }
    
    void OnEnable()
    {
        // Enable Input Actions
        playerInputActions.Enable();
        
        // Subscribe to Input Events
        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnMove;
        
        playerInputActions.Player.Look.performed += OnLook;
        playerInputActions.Player.Look.canceled += OnLook;
        
        playerInputActions.Player.Jump.performed += OnJump;
        playerInputActions.Player.Run.performed += OnRun;
        playerInputActions.Player.Run.canceled += OnRun;
        
        playerInputActions.Player.Crouch.performed += OnCrouch;
    }
    
    void OnDisable()
    {
        // Unsubscribe from Input Events
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnMove;
        
        playerInputActions.Player.Look.performed -= OnLook;
        playerInputActions.Player.Look.canceled -= OnLook;
        
        playerInputActions.Player.Jump.performed -= OnJump;
        playerInputActions.Player.Run.performed -= OnRun;
        playerInputActions.Player.Run.canceled -= OnRun;
        
        playerInputActions.Player.Crouch.performed -= OnCrouch;
        
        // Disable Input Actions
        playerInputActions.Disable();
    }
    
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCrouching();
        HandleJumping();
        ApplyGravity();
        
        characterController.Move(velocity * Time.deltaTime);
        
        isGrounded = characterController.isGrounded;
        
        // ESC для смены курсора
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    
    #region Input Event Handlers
    
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    
    private void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
    }
    
    private void OnRun(InputAction.CallbackContext context)
    {
        runInput = context.performed;
    }
    
    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            crouchInput = !crouchInput;
        }
    }
    
    #endregion
    
    void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lookDownLimit, lookUpLimit);
        
        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
    
    void HandleMovement()
    {
        float horizontal = moveInput.x;
        float vertical = moveInput.y;
        
        Vector3 direction = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
        direction = Vector3.ClampMagnitude(direction, 1f);
        
        float currentSpeed = walkSpeed;
        if (runInput)
            currentSpeed = runSpeed;
        else if (crouchInput)
            currentSpeed = crouchSpeed;
        
        if (isGrounded)
        {
            velocity.x = direction.x * currentSpeed;
            velocity.z = direction.z * currentSpeed;
        }
        else
        {
            velocity.x = Mathf.Lerp(velocity.x, direction.x * currentSpeed, airControl * Time.deltaTime);
            velocity.z = Mathf.Lerp(velocity.z, direction.z * currentSpeed, airControl * Time.deltaTime);
        }
    }
    
    void HandleCrouching()
    {
        targetHeight = crouchInput ? crouchHeight : standingHeight;
        
        if (Mathf.Abs(currentHeight - targetHeight) > 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, crouchTransitionSpeed * Time.deltaTime);
            characterController.height = currentHeight;
            
            if (playerCamera != null)
            {
                float cameraY = crouchInput ? cameraHeight * 0.5f : cameraHeight;
                playerCamera.transform.localPosition = new Vector3(0, cameraY, 0);
            }
        }
    }
    
    void HandleJumping()
    {
        if (jumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            jumpInput = false; // Reset jump input
        }
    }
    
    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }
    
    void OnDestroy()
    {
        playerInputActions?.Dispose();
    }
}
