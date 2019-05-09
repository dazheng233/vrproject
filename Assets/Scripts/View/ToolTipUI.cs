using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTipUI : MonoBehaviour
{
    public Text OutlineText;
    public Text ContentText;

    public void UpdateTooltip(string text,Vector3 position)
    {
        OutlineText.text = text;
        ContentText.text = text;
        OutlineText.color=new Color(0.0f,0.0f,0.0f,0.0f);
        gameObject.transform.localPosition = position;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}

