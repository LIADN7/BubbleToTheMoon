using UnityEngine;

public class TreeEnemy: Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.spawnPos = SpawnPos.Side;
    }



    // Override the Move method, leaving it empty as per your request
    public override void Move()
    {
        // No movement logic for TreeEnemy
    }

    // Override the Die method to prevent the object from being destroyed
    public override void Die(Collider2D other)
    {
        // Logic for dying, without destroying the object
        Debug.Log("TreeEnemy has died, but is not destroyed.");

        // Optionally, disable the enemy or trigger other effects instead of destroying it
        // Example: Disable the enemy's collider and stop movement
/*        GetComponent<Collider2D>().enabled = false;
        isTriggered = false;*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Die(other);
    }
}
