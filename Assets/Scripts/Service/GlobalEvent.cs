using System;
using UnityEngine;

public class GlobalEvent : MonoBehaviour
{
    public static Action StartGame;
    public static Action PauseGame;
    public static Action ResumeGame;
    public static Action RestartGame;

    public static Action<int, int> AddBonus;
}