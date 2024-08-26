namespace Tetris
{
    public class TetrominoViolet : TetrominoBase
    {
        private readonly int[,] _form = {
            { 0, 1, 0 },
            { 1, 1, 0 },
            { 0, 1, 0 },
        };

        public override int[,] Form { get => _form; }
    }
}