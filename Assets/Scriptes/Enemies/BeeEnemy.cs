using UnityEngine;


public class BeeEnemy : Enemy
{
    private float flyHeight = 5f; // The height the bird flies at
    private float horizontalSpeed = 2f; // Horizontal movement speed

    private bool isMovingRight = true; // Determines the current horizontal direction


    void Start()
    {
        this.speed = UnityEngine.Random.Range(4, 7);
        this.flyHeight = UnityEngine.Random.Range(4, 7);
        this.horizontalSpeed = UnityEngine.Random.Range(1, 3);
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// Implements the movement behavior for the bird enemy.
    /// </summary>
    public override void Move()
    {
        // Simulate horizontal movement
        float horizontalMovement = horizontalSpeed * Time.deltaTime * (isMovingRight ? 1 : -1);
        transform.position += new Vector3(horizontalMovement, Mathf.Sin(Time.time * speed) * flyHeight * Time.deltaTime, 0);

        // Flip direction if the bird reaches a boundary
        if (transform.position.x > 3) // Example boundary on the right
        {
            isMovingRight = false;
        }
        else if (transform.position.x < -3) // Example boundary on the left
        {
            isMovingRight = true;
        }
    }

    /// <summary>
    /// Defines the logic for when the bird enemy is hit by another object.
    /// </summary>
    /// <param name="other">The collider of the object that hits the bird.</param>
    public override void OnHit(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("BirdEnemy hit the player!");
            Die(); // Call Die if necessary
        }
    }

    /// <summary>
    /// Implements the death behavior for the bird enemy.
    /// </summary>
    public override void Die()
    {
        Debug.Log("BirdEnemy died!");
        Destroy(gameObject); // Remove the bird enemy from the scene
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Die();
    }
}