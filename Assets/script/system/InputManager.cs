using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsSprinting { get; private set; }
    
    private void OnEnable()
    {
        // Utilise les actions projet-wide automatiquement
        InputSystem.actions.FindAction("Move").performed += OnMove;
        InputSystem.actions.FindAction("Move").canceled += OnMoveCanceled;
        InputSystem.actions.FindAction("Look").performed += OnLook;
        InputSystem.actions.FindAction("Jump").performed += OnJump;
        InputSystem.actions.FindAction("Jump").canceled += OnJumpCanceled;
        InputSystem.actions.FindAction("Sprint").performed += OnSprint;
        InputSystem.actions.FindAction("Sprint").canceled += OnSprintCanceled;
    }
    
    private void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    private void OnLook(InputAction.CallbackContext ctx) => LookInput = ctx.ReadValue<Vector2>();
    private void OnJump(InputAction.CallbackContext ctx) => IsJumping = true;
    private void OnJumpCanceled(InputAction.CallbackContext ctx) => IsJumping = false;
    private void OnSprint(InputAction.CallbackContext ctx) => IsSprinting = true;
    private void OnSprintCanceled(InputAction.CallbackContext ctx) => IsSprinting = false;
    
    private void OnDisable()
    {
        InputSystem.actions.FindAction("Move").performed -= OnMove;
        InputSystem.actions.FindAction("Move").canceled -= OnMoveCanceled;
        InputSystem.actions.FindAction("Look").performed -= OnLook;
        InputSystem.actions.FindAction("Jump").performed -= OnJump;
        InputSystem.actions.FindAction("Jump").canceled -= OnJumpCanceled;
        InputSystem.actions.FindAction("Sprint").performed -= OnSprint;
        InputSystem.actions.FindAction("Sprint").canceled -= OnSprintCanceled;
    }
}