using UnityEngine;


public class MainMenu : BaseMenu
{
    [Header("Panel of main menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header("Start button")]
    [SerializeField] private ButtonUI _startButton;

    [Header("Tap to start text")]
    [SerializeField] private TextUI _tapToStartText;

    private UIController _uiController;

    private void Start()
    {
        _uiController = transform.parent.GetComponentInChildren<UIController>();

        _startButton.GetControl.onClick.AddListener(() => _uiController.StartGame());
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