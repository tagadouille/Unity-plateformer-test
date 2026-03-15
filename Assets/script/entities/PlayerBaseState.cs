using UnityEngine;

// Classe abstraite de base pour tous les états
public abstract class PlayerBaseState
{
    protected PlayerStateMachine context;
    
    public PlayerBaseState(PlayerStateMachine context)
    {
        this.context = context;
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

// États concrets à implémenter
public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine context) : base(context) { }
    
    public override void Enter()
    {
        Debug.Log("Entrée dans l'état Idle");
    }
    
    public override void Update()
    {
        // Logique de l'état Idle
        if (context.Input.MoveInput != Vector2.zero)
        {
            context.TransitionToState(context.WalkingState);
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Sortie de l'état Idle");
    }
}

public class WalkingState : PlayerBaseState
{
    public WalkingState(PlayerStateMachine context) : base(context) { }
    
    public override void Enter()
    {
        Debug.Log("Entrée dans l'état Walking");
    }
    
    public override void Update()
    {
        // Logique de l'état Walking
        if (context.Input.MoveInput == Vector2.zero)
        {
            context.TransitionToState(context.IdleState);
        }
        
        if (context.Input.IsJumping)
        {
            context.TransitionToState(context.JumpingState);
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Sortie de l'état Walking");
    }
}

public class JumpingState : PlayerBaseState
{
    private float jumpTimer;
    private const float JUMP_DURATION = 0.5f;
    
    public JumpingState(PlayerStateMachine context) : base(context) { }
    
    public override void Enter()
    {
        Debug.Log("Entrée dans l'état Jumping");
        jumpTimer = 0f;
        context.ApplyJumpForce();
    }
    
    public override void Update()
    {
        jumpTimer += Time.deltaTime;
        
        if (jumpTimer >= JUMP_DURATION || context.IsGrounded)
        {
            if (context.Input.MoveInput != Vector2.zero)
            {
                context.TransitionToState(context.WalkingState);
            }
            else
            {
                context.TransitionToState(context.IdleState);
            }
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Sortie de l'état Jumping");
    }
}

// Machine à états principale
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerBaseState CurrentState { get; private set; }
    
    // Références aux états
    public IdleState IdleState { get; private set; }
    public WalkingState WalkingState { get; private set; }
    public JumpingState JumpingState { get; private set; }
    
    // Composants nécessaires
    [HideInInspector] public InputManager Input;
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public bool IsGrounded;
    
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    
    private void Awake()
    {
        // Récupération des composants
        Input = GetComponent<InputManager>();
        Rb = GetComponent<Rigidbody>();
        
        // Initialisation des états
        IdleState = new IdleState(this);
        WalkingState = new WalkingState(this);
        JumpingState = new JumpingState(this);
    }
    
    private void Start()
    {
        TransitionToState(IdleState);
    }
    
    private void Update()
    {
        CurrentState?.Update();
        CheckGrounded();
    }
    
    private void FixedUpdate()
    {
        // Logique de mouvement ici (appelée à intervalles fixes pour la physique)
    }
    
    private void CheckGrounded()
    {
        // Raycast pour vérifier si le joueur touche le sol
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
    
    public void ApplyJumpForce()
    {
        if (Rb != null)
        {
            Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    public void TransitionToState(PlayerBaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}