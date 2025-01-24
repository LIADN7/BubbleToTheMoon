using UnityEngine;

    public enum SpawnPos{
        None,
        Side,
        Middle
    }

public abstract class Enemy : MonoBehaviour
{
    protected SpawnPos spawnPos { get; set; } = SpawnPos.None ;
    [SerializeField] protected float speed; // Speed of the enemy movement
    [SerializeField] protected bool isTriggered=false; // Trigger state to control movement

    void Update()
    {
        // If the enemy is triggered, allow it to move
        if (isTriggered)
        {
            Move();
        }
    }


    /// <summary>
    /// Handles the movement of the enemy. Must be implemented by derived classes.
    /// </summary>
    public abstract void Move();


    /// <summary>
    /// Handles the death of the enemy, such as playing animations or destroying the object.
    /// </summary>
    public abstract void Die(Collider2D other);

    /// <summary>
    /// Call this method to trigger the enemy's movement.
    /// </summary>
    public void Trigger(bool flag=true) {
        isTriggered = flag;
    }


}