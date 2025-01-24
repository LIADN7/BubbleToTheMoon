using UnityEngine;


public class BeeEnemy : Enemy
{
   private float flyHeight = 5f; // The height the bird flies at
    private float horizontalSpeed = 2f; // Horizontal movement speed

    private bool isMovingRight = true; // Determines the current horizontal direction


    void Start()
    {
        this.spawnPos = SpawnPos.Middle;
        this.speed = UnityEngine.Random.Range(4, 7);
        this.flyHeight = UnityEngine.Random.Range(4, 7);
        this.horizontalSpeed = UnityEngine.Random.Range(1, 3);
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
    /// Implements the death behavior for the bird enemy.
    /// </summary>
    public override void Die(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //sound
            Destroy(gameObject); // Remove the Bee enemy from the scene
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Die(other);
    }
}