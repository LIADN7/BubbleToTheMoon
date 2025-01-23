using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed; // Speed of the enemy movement

    /// <summary>
    /// Handles the movement of the enemy. Must be implemented by derived classes.
    /// </summary>
    public abstract void Move();

    /// <summary>
    /// Handles logic when the enemy collides with the player or another object.
    /// </summary>
    /// <param name="other">The collider of the object that the enemy collided with.</param>
    public abstract void OnHit(Collider2D other);

    /// <summary>
    /// Handles the death of the enemy, such as playing animations or destroying the object.
    /// </summary>
    public abstract void Die();

}