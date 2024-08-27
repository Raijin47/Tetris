using UnityEngine;

namespace Tetris
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Slot _prefab;

        private const int Size = 9;

        public Slot[,] Slot;

        private void Start()
        {
            InitializeField();
        }

        private void OnEnable() => GlobalEvent.RestartGame += CleanField;
        private void OnDisable() => GlobalEvent.RestartGame -= CleanField;

        private void InitializeField()
        {
            Slot = new Slot[Size, Size];
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    var newSlot = Instantiate(_prefab, _content);
                    newSlot.X = x;
                    newSlot.Y = y;
                    newSlot.Field = this;
                    Slot[x, y] = newSlot;
                }
            }
        }

        public void CheckLine(int line)
        {
            for (int y = line - 1; y <= line + 1; y++)
            {
                if (y < 0 || y > 8) continue;

                bool onClean = true;
                for (int x = 0; x < Size; x++)
                {
                    if (Slot[y, x].Tetromino == null)
                        onClean = false;
                }
                if (onClean) CleanLine(y);
            }
        }

        private void CleanLine(int line)
        {
            for (int x = 0; x < Size; x++)
            {
                var obj = Slot[line, x];
                GlobalEvent.AddBonus?.Invoke(obj.Tetromino.ID, obj.Tetromino.Bonus);
                obj.Tetromino = null;
            }

            Audio.Play(ClipType.tetrominoLine);
        }

        private void CleanField()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Slot[x, y].Tetromino = null;
                }
            }
        }
    }
}