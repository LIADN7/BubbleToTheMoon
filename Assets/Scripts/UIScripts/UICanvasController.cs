using TMPro;
using UnityEngine;
public class UICanvasController : MonoBehaviour
{
    private bool isPaused;
    private bool isMuted = false;
    private GameManager manager;
    [SerializeField] private GameObject WinnerScreen;

    void Start()
    {
        this.manager = GameManager.inst;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        SoundsNames soundName = isPaused ? SoundsNames.PauseButton : SoundsNames.ResumeButton;
        SoundManager.inst.Play(soundName);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void RestartGame()
    {
        SoundManager.inst.Play(SoundsNames.RestartGame);
        this.manager.TriggerRestartGame(0);
    }

    public void ShowWinner(string winnerName)
    {
        SoundManager.inst.Stop(SoundsNames.MainGameMusic);
        SoundManager.inst.Play(SoundsNames.WinSound);
        WinnerScreen.SetActive(true);
        WinnerScreen.GetComponentInChildren<TextMeshProUGUI>().text = $"Player {winnerName} wins!";
    }


    public void MuteAll()
    {
        if (!isMuted)
        {
            SoundManager.inst.Mute();
        }
        else
        {
            SoundManager.inst.Unmute();

        }
        isMuted = !isMuted;
    }

}
