using UnityEngine;

public class TreeEnemy : Enemy
{

    private void Awake()
    {
        this.spawnPos = SpawnPos.Side;
    }

    private void Start()
    {
        if (!rightSide)
            transform.rotation = Quaternion.Euler(0, 0, -180);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Die(other);
    }
}
