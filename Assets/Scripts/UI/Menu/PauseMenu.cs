using UnityEngine;


public class PauseMenu : BaseMenu
{
    [Header("Panel of pause menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header("Resume button")]
    [SerializeField] private ButtonUI _resumeButton;

    private UIController _uiController;

    private void Start()
    {
        _uiController = transform.parent.GetComponentInChildren<UIController>();

        _resumeButton.GetControl.onClick.AddListener(() => _uiController.StartGame());
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