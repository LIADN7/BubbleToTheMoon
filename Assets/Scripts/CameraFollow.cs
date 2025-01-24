using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform[] playerTransforms; // Array of player Transforms
    [SerializeField] private float offsetY = 2f; // Offset above the highest player's Y position

    private float currentCameraY; // To track the camera's current Y position

    private void Start()
    {
        // Initialize the camera's starting Y position
        currentCameraY = transform.position.y;
    }


    private void LateUpdate()
    {
        if (playerTransforms == null || playerTransforms.Length == 0) return;

        // Find the highest Y position among the players
        float highestY = float.MinValue;

        foreach (Transform player in playerTransforms)
        {
            if (player != null && player.position.y > highestY)
            {
                highestY = player.position.y;
            }
        }

        // Calculate the new Y position for the camera
        float targetY = highestY + offsetY;

        // Ensure the camera does not move down
        if (targetY > currentCameraY)
        {
            currentCameraY = targetY;
        }

        // Update the camera's position: only adjust Y, keep X and Z constant
        Vector3 newCameraPosition = new Vector3(transform.position.x, currentCameraY, transform.position.z);
        transform.position = newCameraPosition;
    }
}
