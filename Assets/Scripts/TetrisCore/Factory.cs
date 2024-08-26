using UnityEngine;

namespace Tetris
{
    public class Factory : MonoBehaviour
    {
        public static Color Transparent = new(0, 0, 0, 0);
        public static Vector2 Size = new(0.7f, 0.7f);

        [SerializeField] private TetrominoBase[] _tetromino;
        [SerializeField] private BlockBase[] _blocks;
        [SerializeField] private BlockBase _newBlock;

        public BlockBase NewBlock => _newBlock;

        private void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            _newBlock.Init();
            _newBlock.Factory = this;
            CreateNewBlock();

            for (int i = 0; i < _blocks.Length; i++)
            {
                _blocks[i].Init();
                _blocks[i].Factory = this;
                _blocks[i].CleanTetromino();
            }
        }

        public void CreateNewBlock()
        {
            int random = Random.Range(0, _tetromino.Length);

            _newBlock.Tetromino = _tetromino[random];
            _newBlock.CleanTetromino();
            _newBlock.ViewTetromino();
        }
    }
}