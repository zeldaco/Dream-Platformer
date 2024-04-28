using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 2.0f;
    private float progress = 0.0f;

    private void Update()
    {
        // Calculate next position
        progress += Time.deltaTime * speed; 
        float pingPong = Mathf.PingPong(progress, 1);
        transform.position = Vector2.Lerp(pointA, pointB, pingPong);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.activeSelf) // Check if MovingPlatform is active
        {
            // Set the parent of collision object to null
            collision.transform.SetParent(null);
        }
    }
}
