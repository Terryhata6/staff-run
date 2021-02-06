using UnityEngine;


public class InGameUI : BaseMenu
{
    [Header("Panel of ingame UI")]
    [SerializeField] private GameObject _mainPanel;

    [Header("Coins text")]
    [SerializeField] private TextUI _coinsText;

    [Header("Button pause")]
    [SerializeField] private ButtonUI _pauseButton;

    private UIController _uiController;
    private CoinManager _coinManager;

    private void Start()
    {

        _uiController = transform.parent.GetComponentInChildren<UIController>();
        _coinManager = FindObjectOfType<CoinManager>();

        _pauseButton.GetControl.onClick.AddListener(() => _uiController.PauseGame());
    }

    private void Update()
    {
        _coinsText.GetControl.text = $"{_coinManager.GetCurrentCoin()}";
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
