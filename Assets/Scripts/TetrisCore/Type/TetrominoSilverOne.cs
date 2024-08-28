namespace Tetris
{
    public class TetrominoSilverOne : TetrominoBase
    {
        private readonly int[,] _form = {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 },
        };

        public override int[,] Form { get => _form; }
    }
}