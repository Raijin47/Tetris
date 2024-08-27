using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Sub-Classes
    [System.Serializable]
    public class UIButtonEvent : UnityEvent<PointerEventData.InputButton> { }
    #endregion

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private bool _pressed = true;

    private readonly Vector2 PressedSize = new(0.9f, 0.9f);

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (_pressed)
            _rectTransform.localScale = PressedSize;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (_pressed)
            _rectTransform.localScale = Vector2.one;
    }

    private void OnValidate()
    {
        _rectTransform ??= GetComponent<RectTransform>();
    }
}