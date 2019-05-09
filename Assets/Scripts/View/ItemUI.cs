using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ItemUI : MonoBehaviour
{
    public Image itemImage;

    private void Start()
    {
//        itemImage = GetComponent<Image>();
    }

    public void UpdateImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}
