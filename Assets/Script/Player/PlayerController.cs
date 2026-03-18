using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform bottomCollide;

    [SerializeField] private LayerMask ground;
    private Rigidbody rb;
    private Transform location;

    private bool onGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        location = GetComponent<Transform>();
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

        Vector3 velocityMove = rb.linearVelocity;
        velocityMove.x = speed * horizontal;
        velocityMove.z = speed * vertical;

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
