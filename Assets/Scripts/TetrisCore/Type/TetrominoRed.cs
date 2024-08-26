namespace Tetris
{
    public class TetrominoRed : TetrominoBase
    {
        private readonly int[,] _form = {
            { 0, 1, 0 },
            { 0, 1, 0 },
            { 0, 1, 1 },
        };

        public override int[,] Form { get => _form; }
    }
}