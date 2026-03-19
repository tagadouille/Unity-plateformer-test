using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform bottomCollide;

    [SerializeField] private LayerMask ground;
    private Rigidbody rb;

    private bool onGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(onGround);
        }
        // Jump
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;
        }

        // Move
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a movement vector relative to the player
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        
        // Transform in world space using player rotation
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);

        Vector3 velocityMove = rb.linearVelocity;
        velocityMove.x = speed * worldMoveDirection.x;
        velocityMove.z = speed * worldMoveDirection.z;

        rb.linearVelocity = velocityMove;
    }

    void FixedUpdate()
    {
        onGround = IsOnGround();
    }

    bool IsOnGround()
    {
        return Physics.CheckSphere(bottomCollide.position, 0.25f, ground);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy head"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
