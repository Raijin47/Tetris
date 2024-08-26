using UnityEngine;

namespace Tetris
{
    public abstract class TetrominoBase : MonoBehaviour
    {
        public Sprite SpriteImage;
        public abstract int[,] Form { get; } 
    }
}