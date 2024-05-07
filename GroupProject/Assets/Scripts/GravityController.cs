using System.Collections;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool IsGravityUp { get; private set; } = false;
    private Quaternion targetRotation;
    private Transform characterTransform;

    private float gravityFlipCooldown = 1.0f;
    private float lastGravityFlipTime = -2.0f;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;  // Added to access PlayerMovement

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerMovement = GetComponent<PlayerMovement>();  // Get the PlayerMovement component
    }
    
    private void Start()
    {
        characterTransform = this.transform;
        targetRotation = characterTransform.rotation;
        ResetGravity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastGravityFlipTime >= gravityFlipCooldown && playerMovement.IsGrounded()) // Check if grounded before flipping
        {
            lastGravityFlipTime = Time.time;
            FlipGravity();
        }

        characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation, Time.deltaTime * 5);
    }

    private void FlipGravity()
    {
        IsGravityUp = !IsGravityUp;

        if (IsGravityUp)
        {
            Physics2D.gravity = new Vector2(0, 9.8f);
            targetRotation = Quaternion.Euler(180, 0, 0);
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        audioManager.PlaySFX(audioManager.gravityFlip);
    }

    public void ResetGravity()
    {
        IsGravityUp = false;
        Physics2D.gravity = new Vector2(0, -9.81f);
        targetRotation = Quaternion.Euler(0, 0, 0);
    }
}
