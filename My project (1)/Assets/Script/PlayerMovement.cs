using UnityEngine:
using System.Collections;

public class PlayerController : MonoBehavior 
{
    // Movement
    public float speed;
    public float jump;
    float moveVelocity;
    public Rigidbody2D rb;
    bool isGrounded;

    private Vector2 movement = Vector3.left * 0.1f;

    void Update () 
    {

        //Move camera screen with player
        if (player.position.x > 1) 
            movement = vector3.left * 0.1f;
        else if (player.position.x < -1)
            movement = Vector3.right * 0.1f;
        transform.Translate(movement);

        // Grounded?
        if (isGrounded == true) 
        {
            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(Update.Arrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)){
                
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }
        }
        
        moveVelocity = 0;
        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            moveVelocity = -speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
        {
            moveVelocity = speed;
        }

        GetComponent<Rigidbody2D>.velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }


    void OnCollisionEnter2D(Collision2D col) 
    {
        Debug.Log("OnCollisionEnter2D");
        isGrounded = true;
    }
    void onCollisionExit2D(Collision2D col) 
    {
        Debug.Log("OnCollisionExit2D");
        isGrounded = false;
    }
}