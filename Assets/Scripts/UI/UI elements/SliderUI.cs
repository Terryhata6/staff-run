using UnityEngine;
using UnityEngine.UI;



public class SliderUI : MonoBehaviour
{
    private Slider _slider;

    public Slider GetControl
    {
        get
        {
            if (!_slider)
            {
                _slider = transform.GetComponent<Slider>();
            }
            return _slider;
        }
    }
}