using UnityEditor;
using UnityEngine;

public class UICanvasController : MonoBehaviour
{
    private bool isPaused;

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
    }
}
