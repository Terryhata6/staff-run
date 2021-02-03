using UnityEngine;


public class EndGameMenu : BaseMenu
{
    [Header("Panel of end game menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header("In case of WIN")]
    [SerializeField] private RoadMapController _roadMap;
    [SerializeField] private ButtonUI _nextLevelButton;

    [Header("In case of LOSE")]
    [SerializeField] private ButtonUI _retryButton;

    private UIController _uiController;

    private void Start()
    {
        _uiController = transform.parent.GetComponentInChildren<UIController>();

        _nextLevelButton.GetControl.onClick.AddListener(() => _uiController.NextLevel());
        _retryButton.GetControl.onClick.AddListener(() => _uiController.RestartLevel());
    }

    public void ActivateMenu(bool isLevelComplete, int levelNumber)
    {
        if (isLevelComplete == true)
        {
            _roadMap.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(true);
            _retryButton.gameObject.SetActive(false);

            _roadMap.PaintLevels(levelNumber);
        }
        else if (isLevelComplete == false)
        {
            _roadMap.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _retryButton.gameObject.SetActive(true);
        }
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