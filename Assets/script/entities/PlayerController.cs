using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -9.81f;
    
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;
    
    private CharacterController controller;
    private InputManager input;
    private Vector3 velocity;
    private float currentSpeed;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool isGrounded;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
        
        // Verrouiller le curseur
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleGravity();
    }
    
    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        // Déterminer la vitesse (marche ou sprint)
        currentSpeed = input.IsSprinting ? sprintSpeed : walkSpeed;
        
        // Calculer le mouvement
        Vector3 move = transform.right * input.MoveInput.x + transform.forward * input.MoveInput.y;
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        // Saut
        if (input.IsJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    
    private void HandleRotation()
    {
        // Rotation horizontale du joueur (Y)
        transform.Rotate(Vector3.right * input.LookInput.y * mouseSensitivity);

        // Rotation horizontale du joueur (X)
        transform.Rotate(Vector3.up * input.LookInput.x * mouseSensitivity);
        
        // Rotation verticale de la caméra (X)
        xRotation -= input.LookInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        // Rotation horizontale de la caméra (Y)
        yRotation -= input.LookInput.y * mouseSensitivity;
        yRotation = Mathf.Clamp(yRotation, -maxLookAngle, maxLookAngle);
        
        cameraTarget.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
    
    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}