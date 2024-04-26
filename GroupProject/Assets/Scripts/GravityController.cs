using System.Collections;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool IsGravityUp { get; private set; } = false;  // Directly use this property for gravity direction
    private Quaternion targetRotation;
    private Transform characterTransform;

    private float gravityFlipCooldown = 1.0f; // Time in seconds between gravity flips
    private float lastGravityFlipTime = -2.0f; // Initialize to ensure gravity can flip at start

    private void Start()
    {
        characterTransform = this.transform;
        targetRotation = characterTransform.rotation;
        ResetGravity();  // Ensure gravity is set to default at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastGravityFlipTime >= gravityFlipCooldown)
        {
            lastGravityFlipTime = Time.time; // Update the last flip time
            FlipGravity();
        }

        // Gradually rotate the character to the target rotation
        characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation, Time.deltaTime * 5);
    }

    private void FlipGravity()
    {
        IsGravityUp = !IsGravityUp; // Toggle the state of gravity

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
    }

    public void ResetGravity()
    {
        IsGravityUp = false;  // Ensure the property is updated
        Physics2D.gravity = new Vector2(0, -9.81f);
        targetRotation = Quaternion.Euler(0, 0, 0);  // Reset rotation if needed
    }
}
