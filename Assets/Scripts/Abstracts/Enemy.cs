using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed; // Speed of the enemy movement

    /// <summary>
    /// Handles the movement of the enemy. Must be implemented by derived classes.
    /// </summary>
    public abstract void Move();


    /// <summary>
    /// Handles the death of the enemy, such as playing animations or destroying the object.
    /// </summary>
    public abstract void Die(Collider2D other);

}