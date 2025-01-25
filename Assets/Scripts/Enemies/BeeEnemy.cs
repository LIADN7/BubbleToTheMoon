using UnityEngine;

public class BeeEnemy : Enemy
{
    private float flyHeight = 5f; // The height the bird flies at
    private float horizontalSpeed = 2f; // Horizontal movement speed
    private bool isMovingRight = true; // Determines the current horizontal direction
    private float minSpeed = 4f, maxSpeed = 7f;
    private bool isDead = false;

    private void Awake()
    {
        this.spawnPos = SpawnPos.Middle;

    }
    void Start()
    {
        this.speed = Random.Range(minSpeed, maxSpeed);
        this.flyHeight = Random.Range(minSpeed, maxSpeed);
        this.horizontalSpeed = Random.Range(1, 3);

    }


    protected override void Move()
    {
        // Simulate horizontal movement
        float horizontalMovement = horizontalSpeed * Time.deltaTime * (isMovingRight ? 1 : -1);
        transform.position += new Vector3(horizontalMovement, Mathf.Sin(Time.time * speed) * flyHeight * Time.deltaTime, 0);

        // Flip direction if the bird reaches a boundary
        if (transform.position.x > 3) // Example boundary on the right
            isMovingRight = false;

        else if (transform.position.x < -3) // Example boundary on the left
            isMovingRight = true;
    }

    protected override void Die(Collider2D other)
    {

        base.Die(other);
        isTriggered = false;
        isDead = true;
        SoundManager.inst.Play(SoundsNames.BeesExplotion);

    }

    protected override void Update()
    {
        base.Update();
        if (isDead)
        {
            transform.position += Vector3.down * speed * 2 * Time.deltaTime;

        }
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CameraTop"))
        {
            Trigger();
            SoundManager.inst.PlayOneShot(SoundsNames.BeesIdle, Random.Range(0, 4));
        }
        if (other.CompareTag("Player"))
        {
            //sound
            // Stop movement
            Die(other);
        }
    }
}