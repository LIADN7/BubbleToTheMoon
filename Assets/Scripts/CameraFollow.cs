using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static GameManager;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private List<Transform> playerTransforms = new List<Transform>(); // Array of player Transforms
    [SerializeField] private float offsetY = 2f; // Offset above the highest player's Y position

    private float currentCameraY; // To track the camera's current Y position
    private GameManager manager;
    private void Start()
    {
        manager = GameManager.inst;
        SoundManager.inst.Play(SoundsNames.MainMenuMusic);
        FindAllPlayers();

        // Initialize the camera's starting Y position
        // currentCameraY = transform.position.y;
    }

    private void FindAllPlayers()
    {
        Player[] playersPlaying = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (Player player in playersPlaying)
            if (player != null)
                playerTransforms.Add(player.transform);
    }
    private void LateUpdate()
    {
        if (manager.IsState(GameState.Play))
        {

            if (playerTransforms == null || playerTransforms.Count == 0) return;

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
}
