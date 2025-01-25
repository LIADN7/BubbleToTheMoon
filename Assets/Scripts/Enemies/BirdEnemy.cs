using System.Collections;
using UnityEngine;

public class BirdEnemy : Enemy
{
    private bool moveUpwards = false; // Determines if the bird moves diagonally up
    private float diagonalAngle = 15f; // Angle of diagonal movement in degrees
    private float minSpeed = 4f, maxSpeed = 7f;

    private void Awake()
    {
        this.spawnPos = SpawnPos.Side;
    }

    private void Start()
    {
        this.speed = Random.Range(minSpeed, maxSpeed);
    
        if (!rightSide)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    protected override void Move()
    {
        transform.position += (rightSide ? -1 : 1) * Vector3.right * speed * Time.deltaTime;
        if (IsOutOfScreen())
            Destroy(gameObject);
    }

    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < -0.7f || screenPoint.x > 1.7f || screenPoint.y < -0.7f || screenPoint.y > 1.7f;
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("is touching " + other.gameObject);

        if (other.gameObject.CompareTag("CameraTop"))
        {
            Trigger();
        }
        if (isTriggered)
        {
            StartCoroutine(StartSoundAfter(Random.Range(0.5f, 0.8f)));

        }
        Die(other);
    }

    IEnumerator StartSoundAfter(float sec)
    {
        yield return new WaitForSeconds(sec);
        SoundManager.inst.Play(SoundsNames.BirdIdle);
    }

}