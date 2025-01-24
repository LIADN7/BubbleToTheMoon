using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class UICanvasController : MonoBehaviour
{
    private bool isPaused;
    private GameManager manager;

    void Start()
    {
        this.manager = GameManager.inst;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        SoundsNames soundName = isPaused?SoundsNames.PauseButton: SoundsNames.ResumeButton;
        SoundManager.inst.Play(soundName);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void RestartGame(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SoundManager.inst.Play(SoundsNames.RestartGame);
            this.manager.ChangeState(GameState.Idle);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
