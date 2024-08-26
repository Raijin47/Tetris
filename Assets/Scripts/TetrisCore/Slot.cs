using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tetris
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image _image;

        public Image Image { private set; get; }
        public int X { get; set; }
        public int Y { get; set; }
        public Field Field { get; set; }

        private TetrominoBase _tetromino;
        public TetrominoBase Tetromino 
        { 
            get => _tetromino;
            set 
            {
                _tetromino = value;
                _image.color = _tetromino != null ? Color.white : Factory.Transparent;
                _image.sprite = Tetromino.SpriteImage;
            } 
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var dropped = eventData.pointerDrag.GetComponent<MovingBlock>();

            if(IsFreeSlot(dropped)) AddSlots(dropped);
        }
        private bool IsFreeSlot(MovingBlock tetromino)
        {
            bool free = true;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (tetromino.Tetromino.Form[x, y] != 0)
                    {
                        var a = X - 1 + x;
                        var b = Y - 1 + y;

                        if(a < 0 || b < 0 || a > 8 || b > 8) return false;

                        if (Field.Slot[a, b].Tetromino != null)
                            free = false;
                    }
                }
            }

            return free;
        }

        private void AddSlots(MovingBlock tetromino)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (tetromino.Tetromino.Form[x, y] != 0)
                    {
                        var slot = Field.Slot[X - 1 + x, Y - 1 + y];
                        slot.Tetromino = tetromino.Tetromino;
                    }
                }
            }

            tetromino.CleanTetromino();
            Field.CheckLine(X);
        }
    }
}