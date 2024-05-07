using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private Vector2 startingPosition;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        startingPosition = transform.localPosition;
        SetMovementBounds();
    }

    private void Update()
    {
        Move();
    }

    private void SetMovementBounds()
    {
        leftEdge = startingPosition.x - movementDistance;
        rightEdge = startingPosition.x + movementDistance;
    }

    private void Move()
    {
        if (movingLeft)
        {
            if (transform.localPosition.x > leftEdge)
            {
                transform.localPosition += Vector3.left * speed * Time.deltaTime;
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.localPosition.x < rightEdge)
            {
                transform.localPosition += Vector3.right * speed * Time.deltaTime;
            }
            else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("MovingPlatform"))
    {
        transform.SetParent(collision.transform, true);
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);  // Adjust Y position if necessary
    }
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            startingPosition = transform.position;
            SetMovementBounds();
        }
    }
}