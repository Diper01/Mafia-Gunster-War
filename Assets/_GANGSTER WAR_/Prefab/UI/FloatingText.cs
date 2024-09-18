using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text floatingText;
    Vector2 currentPos;

    public void SetText(string text, Color color)
    {
        floatingText.color = color;
        floatingText.text = text;
    }

    public void SetText(string text, Color color, Vector2 worldPos)
    {
        floatingText.color = color;
        floatingText.text = text;
        currentPos = worldPos;
    }

    void Update()
    {
        //always stay the first position
        var _position = Camera.main.WorldToScreenPoint(currentPos);
        transform.position = _position;
    }
}
