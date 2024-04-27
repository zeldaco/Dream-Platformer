using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // for restarting level

public class EnemyReaction : MonoBehavior 
{
    Rigidbody2D enemyRigidBody2D;
    public int UnitsToMove = 5;
    public float EnemySpeed = 500;
    public bool _isFacingRight;
    private float _startPos;
    private float _endPos;
    public bool _moveRight = true;

    // Use for initialization
    public void Awake() 
    {
        enemyRigidBody2D = GetComponent<enemyRigidBody2D>();
        _startPos = transform.position.x;
        _endPos = _startPos + UnitsToMove;
        _isFacingRight = transform.localScale.x > 0;
    }

    void Start () 
    {

    }

    // Update called once per frame
    void Update() 
    {
        if (_moveRight) 
        {
            enemyRigidBody2D.AddForce(vector2.right * EnemySpeed * Time.deltaTime);
            if (!_isFacingRight)
                Flip();
        }
        
        if (enemyRigidBody2D.position.x >= _endPos)
        {
            _moveRight = false;
        }

        if (!_moveRight) 
        {

            enemyRigidBody2D.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
            if (_isFacingRight)
                Flip();
        }

        if (enemyRigidBody2D.position.x <= _startPos)
        {
            _moveRight = true;
        }

    }

    public void Flip () 
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    // Called when another collider overlaps the trigger collider
    void OnTriggerEnter2D (Collider2D other) 
    {
        // If the overlaped collider is an enemy
        if (other.CompareTag ("Enemy")) 
        {
            // SCENE HAS TO BE IN BUILD SETTINGS
            SceneManager.LoadScene("scene1");

        }
    }
}