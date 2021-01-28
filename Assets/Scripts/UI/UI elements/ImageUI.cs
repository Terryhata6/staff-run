using UnityEngine;
using UnityEngine.UI;


public class ImageUI : MonoBehaviour
{
    private Image _image;

    public Image GetControl
    {
        get
        {
            if (!_image)
            {
                _image = transform.GetComponent<Image>();
            }
            return _image;
        }
    }
}