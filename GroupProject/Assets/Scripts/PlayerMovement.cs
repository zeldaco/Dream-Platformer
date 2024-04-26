using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private GravityController gravityController; // Reference to the GravityController

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravityController = GetComponent<GravityController>(); // Get the GravityController component
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Apply horizontal movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Handle orientation based on input
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Animation control
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f && isGrounded());
        anim.SetBool("grounded", isGrounded());

        // Jump handling
        if (Input.GetKey(KeyCode.Space))
            Jump();

        // Wall contact overrides to begin falling
        if (onWall() && !isGrounded())
        {
            float fallVelocity = gravityController.IsGravityUp ? 9.8f : -9.8f; // Assuming 9.8 is your gravity value
            body.velocity = new Vector2(body.velocity.x, fallVelocity); // Apply immediate fall velocity
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower * (gravityController.IsGravityUp ? -1 : 1));
            anim.SetTrigger("jump");
        }
    }

    private bool isGrounded()
    {
        Vector2 castDirection = gravityController.IsGravityUp ? Vector2.up : Vector2.down;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, castDirection, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(direction, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
