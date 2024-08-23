using System;
using UnityEngine;

public class GlobalEvent : MonoBehaviour
{
    public static Action OnDeliveredChunk;
    public static Action Left;
    public static Action Right;
    public static Action Rotate;

    public static Action StartGame;
    public static Action PauseGame;
    public static Action ResumeGame;
    public static Action RestartGame;
}