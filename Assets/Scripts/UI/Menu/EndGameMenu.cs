using UnityEngine;


public class EndGameMenu : BaseMenu
{
    [Header("Panel of end game menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header("Buttons")]
    [SerializeField] private ButtonUI _nextLevelButton;

    [Header("Slider")]
    [SerializeField] private SliderUI _slider;

    private UIController _uiController;

    private void Start()
    {
        _uiController = transform.parent.GetComponentInChildren<UIController>();

        _nextLevelButton.GetControl.onClick.AddListener(() => _uiController.NextLevel());
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _mainPanel.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _mainPanel.gameObject.SetActive(true);
        IsShow = true;
    }
}