using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class BlockBase : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _prefab;

        private Image[,] _blockImage;

        private const int Size = 3;

        public TetrominoBase Tetromino { get; set; }
        public Factory Factory { get; set; }

        public virtual void Init()
        {
            _blockImage = new Image[Size, Size];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    var icon = Instantiate(_prefab, _content).GetComponent<Image>();
                    _blockImage[x, y] = icon;
                }
            }
        }

        public void ViewTetromino()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (Tetromino.Form[x, y] != 0)
                    {
                        _blockImage[x, y].sprite = Tetromino.SpriteImage;
                        _blockImage[x, y].color = Color.white;
                    }
                }
            }
        }

        public virtual void CleanTetromino()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                    _blockImage[x, y].color = Factory.Transparent;
            }

            Tetromino = Factory.NewBlock.Tetromino;
            ViewTetromino();
        }
    }
}