using UnityEngine;
using UnityEngine.UI;



public class ButtonUI : MonoBehaviour
{
    private Button _button;

    public Button GetControl
    {
        get
        {
            if (!_button)
            {
                _button = transform.GetComponent<Button>();
            }
            return _button;
        }
    }
}