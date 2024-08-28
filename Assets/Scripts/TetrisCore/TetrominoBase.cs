using UnityEngine;

namespace Tetris
{
    public abstract class TetrominoBase : MonoBehaviour
    {
        public int ID;
        public int Bonus;
        public Sprite SpriteImage;

        public abstract int[,] Form { get; }

        public int[,] RotatePiece(int[,] piece)
        {
            int n = piece.GetLength(0);
            int[,] rotatedPiece = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    rotatedPiece[j, n - 1 - i] = piece[i, j];
                }
            }

            return rotatedPiece;
        }
    }
}