using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        GlobalEvent.StartGame?.Invoke();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GlobalEvent.PauseGame?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        GlobalEvent.ResumeGame?.Invoke();
    }

    public void RestartGame()
    {
        GlobalEvent.RestartGame?.Invoke();
    }
}