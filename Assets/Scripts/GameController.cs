using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        GlobalEvent.StartGame?.Invoke();
    }
}