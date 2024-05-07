using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;             // Player movement speed
    [SerializeField] private float jumpPower;        // Power of the jump
    [SerializeField] private LayerMask groundLayer;   // Layer to detect ground

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private GravityController gravityController;     // Reference to the GravityController
    private AudioManager audioManager;               // Reference to the AudioManager

    private void Awake()
    {
        // Get the necessary components on awake
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravityController = GetComponent<GravityController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        // Read horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Apply horizontal movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Handle orientation based on input direction
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Update animation states
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f && isGrounded());
        anim.SetBool("grounded", isGrounded());

        // Jump handling
        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        // Jump if grounded
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower * (gravityController.IsGravityUp ? -1 : 1));
            anim.SetTrigger("jump");
            audioManager.PlaySFX(audioManager.jump);  // Play jump sound effect
        }
    }

    private bool isGrounded()
    {
        // Check if the player is grounded
        Vector2 castDirection = gravityController.IsGravityUp ? Vector2.up : Vector2.down;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, castDirection, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
