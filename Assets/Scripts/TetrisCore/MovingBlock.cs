using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tetris
{
    public class MovingBlock : BlockBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector2 _startPosition;
        private Image _image;

        public override void Init()
        {
            _startPosition = transform.position;
            _image = GetComponent<Image>();

            base.Init();
        }

        public override void CleanTetromino()
        {
            base.CleanTetromino();
            Factory.CreateNewBlock();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _image.raycastTarget = false;
            transform.localScale = Vector3.one;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _image.raycastTarget = true;
            transform.position = _startPosition;

            transform.localScale = Factory.Size;
        }
    }
}