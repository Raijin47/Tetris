using UnityEngine;
using UnityEngine.UI;

public class VisualPercent : MonoBehaviour
{
    public int maxCount = 10;
    public Image image;
    public float percent;
    public bool visualImage = true;
    public bool visualScaleX = false;

    public void Set(float value)
    {
        percent = value;

        if (visualImage)
            image.fillAmount = percent;

        if (visualScaleX)
        {
            Vector3 scale = transform.localScale;
            scale.x = percent;
            transform.localScale = scale;
        }
    }

    public void SetInt(int value)
    {
        float percent = Mathf.Clamp((float)value / maxCount, 0, 1);
        Set(percent);
    }

    public void SetCount(int count)
    {
        maxCount = count;
    }

    private void OnValidate()
    {
        if (visualImage && image == null)
            image = GetComponent<Image>();
    }
}
