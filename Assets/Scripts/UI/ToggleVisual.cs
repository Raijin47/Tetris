using TMPro;
using UnityEngine;

public class ToggleVisual : MonoBehaviour
{
    public TextMeshProUGUI t_text;
    public string[] text = { "OFF", "ON" };

    public void Set(bool value)
    {
        int id = value ? 1 : 0;
        t_text.text = text[id];
    }
}
