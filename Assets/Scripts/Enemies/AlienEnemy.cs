using System.Collections;
using UnityEngine;

public class AlienEnemy : Enemy
{
    private float minSpeed = 6f, maxSpeed = 10f;
    private float minRotate = -10f, maxRotate = 10f;


    private void Awake()
    {
        this.spawnPos = SpawnPos.Side;
    }

    private void Start()
    {
        this.speed = Random.Range(minSpeed, maxSpeed);
        if (!rightSide)
            Flip();
    }

    private void Flip()
    {
        transform.rotation = Quaternion.Euler(0, (rightSide ? 0 : 180), 0);
    }

    protected override void Move()
    {
        transform.position += (rightSide ? -1 : 1) * Vector3.right * speed * Time.deltaTime;
        if (IsOutOfScreen())
        {
            rightSide = !rightSide;
            Flip();
        }
    }

    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < -0.2f || screenPoint.x > 1.2f || screenPoint.y < -0.7f || screenPoint.y > 1.7f;
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CameraTop"))
        {
            Trigger();

            // SoundManager.inst.PlayOneShot(SoundsNames, Random.Range(0, 4));

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
        // SoundManager.inst.Play(SoundsNames.BirdIdle);
    }

}