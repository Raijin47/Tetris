using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        GlobalEvent.OnDeliveredChunk?.Invoke();
        GlobalEvent.StartGame?.Invoke();
    }
}