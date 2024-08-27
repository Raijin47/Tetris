using UnityEngine;

namespace Tetris
{
    public abstract class TetrominoBase : MonoBehaviour
    {
        public int ID;
        public int Bonus;
        public Sprite SpriteImage;

        public abstract int[,] Form { get; } 
    }
}