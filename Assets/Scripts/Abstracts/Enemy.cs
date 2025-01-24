using UnityEngine;

public enum SpawnPos
{
    None,
    Side,
    Middle
}

public class Enemy : MonoBehaviour
{
    [SerializeField] public SpawnPos spawnPos;
    [SerializeField] protected float speed; // Speed of the enemy movement
    [SerializeField] protected bool isTriggered = false; // Trigger state to control movement
    [SerializeField] public bool rightSide = true;

    protected virtual void Update()
    {
        // If the enemy is triggered, allow it to move
        if (isTriggered)
            Move();
    }

    protected virtual void Move() { }

    protected virtual void Die(Collider2D other) { }

    protected virtual void Trigger(bool flag = true)
    {
        isTriggered = flag;
    }

}