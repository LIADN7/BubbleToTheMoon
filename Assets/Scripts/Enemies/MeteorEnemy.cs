using System.Collections;
using UnityEngine;

public class MeteorEnemy : Enemy
{
    private float minSpeed = 7f, maxSpeed = 10f;
    private float minRotate = -0.2f, maxRotate = -0.5f;


    private void Awake()
    {
        this.spawnPos = SpawnPos.Side;
    }

    private void Start()
    {
        this.speed = Random.Range(minSpeed, maxSpeed);
        transform.rotation = Quaternion.Euler(0, rightSide ? 0 : 180, 0);

    }


    protected override void Move()
    {
        transform.position += (rightSide ? -1 : 1) * new Vector3(1, (rightSide ? -1 : 1) * Random.Range(minRotate, maxRotate), 0) * speed * Time.deltaTime;
    }



    protected void OnTriggerEnter2D(Collider2D other)
    {
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
        // SoundManager.inst.Play(SoundsNames.BirdIdle);
    }

}