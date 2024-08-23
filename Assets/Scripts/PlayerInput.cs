using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) GlobalEvent.Left?.Invoke();
        if (Input.GetKeyDown(KeyCode.RightArrow)) GlobalEvent.Right?.Invoke();
        if (Input.GetKeyDown(KeyCode.UpArrow)) GlobalEvent.Rotate?.Invoke();
    }
}