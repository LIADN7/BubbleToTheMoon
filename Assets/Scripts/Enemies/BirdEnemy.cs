using Unity.VisualScripting;
using UnityEngine;

public class BirdEnemy : Enemy
{
    private bool moveRightToLeft = true; // Determines if the bird moves from right to left
    private bool moveUpwards = false; // Determines if the bird moves diagonally up
    private float diagonalAngle = 45f; // Angle of diagonal movement in degrees

    private Vector3 movementDirection;

    private void Start()
    {
        this.speed = UnityEngine.Random.Range(3, 6);
        int randNum = UnityEngine.Random.Range(0, 2);
        this.moveRightToLeft = randNum == 0;

        // Calculate initial movement direction based on angle and movement flags
        float angle = moveUpwards ? diagonalAngle : -diagonalAngle;
        angle = moveRightToLeft ? 180f - angle : angle;

        //GetComponent<SpriteRenderer>().flipX= !this.moveRightToLeft;
        transform.rotation = Quaternion.Euler(0, 0, -1*angle) ;
        movementDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Handles diagonal movement for the bird.
    /// </summary>
    public override void Move()
    {
        // Move the bird based on the calculated direction
        transform.position += movementDirection * speed * Time.deltaTime;

        // Destroy the bird if it goes off-screen
        if (IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Determines if the bird is outside the screen boundaries.
    /// </summary>
    /// <returns>True if the bird is out of the screen, false otherwise.</returns>
    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f;
    }

    /// <summary>
    /// Handles the death of the bird, triggered by collisions.
    /// </summary>
    /// <param name="other">The collider that triggered the death.</param>
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