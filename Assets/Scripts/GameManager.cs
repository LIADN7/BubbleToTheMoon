using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Enum representing the different game states
    public enum GameState
    {
        Idle,
        Play,
        Endgame,

    }

    // Static instance for global access
    public static GameManager inst;

    // Current state of the game
    [SerializeField] private GameState currentState = GameState.Idle;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (inst == null)
            inst = this;
        else
            Destroy(gameObject);

        // Optionally make the GameManager persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    public GameState CurrentState => currentState;

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game state changed to: {currentState}");
        if (currentState == GameState.Endgame)
        {
            TriggerRestartGame(3);
        }
    }

    public bool IsState(GameState state)
    {
        return currentState == state;
    }

    public void TriggerRestartGame(int secondsBeforeRestart)
    {
        StartCoroutine(Countdown(secondsBeforeRestart));

    }


    private IEnumerator Countdown(int seconds)
    {
        float currentTime = seconds;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        RestartGame();
    }

    private void RestartGame()
    {
        ChangeState(GameState.Idle);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }


}