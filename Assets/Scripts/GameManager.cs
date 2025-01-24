using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Enum representing the different game states
    public enum GameState
    {
        Idle,
        Play,
        Win,
        Loss
    }

    // Static instance for global access
    public static GameManager inst;

    // Current state of the game
    [SerializeField] private GameState currentState = GameState.Idle;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Optionally make the GameManager persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Get the current game state.
    /// </summary>
    public GameState CurrentState => currentState;

    /// <summary>
    /// Change the game state.
    /// </summary>
    /// <param name="newState">The new state to set.</param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game state changed to: {currentState}");
    }

    /// <summary>
    /// Checks if the game is in a specific state.
    /// </summary>
    /// <param name="state">The state to check.</param>
    /// <returns>True if the game is in the specified state; otherwise, false.</returns>
    public bool IsState(GameState state)
    {
        return currentState == state;
    }



}