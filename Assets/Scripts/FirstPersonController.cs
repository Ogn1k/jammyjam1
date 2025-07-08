using UnityEngine;
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
    
    [Header("Input Settings")]
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    
    [Header("Advanced Settings")]
    public float airControl = 0.3f;
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2f;
    public float crouchTransitionSpeed = 10f;
    
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float rotationX = 0f;
    private bool isCrouching = false;
    private float currentHeight;
    private float targetHeight;
    
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
    
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCrouching();
        HandleJumping();
        ApplyGravity();
        
        characterController.Move(velocity * Time.deltaTime);
        
        isGrounded = characterController.isGrounded;
        
        if (Input.GetKeyDown(KeyCode.Escape))
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
    
    void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
            
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lookDownLimit, lookUpLimit);
        
        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
    
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
        direction = Vector3.ClampMagnitude(direction, 1f);
        
        float currentSpeed = walkSpeed;
        if (Input.GetKey(runKey))
            currentSpeed = runSpeed;
        else if (isCrouching)
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
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            targetHeight = isCrouching ? crouchHeight : standingHeight;
        }
        
        if (Mathf.Abs(currentHeight - targetHeight) > 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, crouchTransitionSpeed * Time.deltaTime);
            characterController.height = currentHeight;
            
            if (playerCamera != null)
            {
                float cameraY = isCrouching ? cameraHeight * 0.5f : cameraHeight;
                playerCamera.transform.localPosition = new Vector3(0, cameraY, 0);
            }
        }
    }
    
    void HandleJumping()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
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
}
