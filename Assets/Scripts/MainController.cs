using UnityEngine;
using UnityEngine.SceneManagement;


public class MainController : MonoBehaviour
{
    [SerializeField] private int _levelNumber;

    private LevelBuilder _levelBuilder;
    private SaveDataRepo _saveData;
    private UIController _uiController;

    private CoinManager _coinManager;
    private int _coins;


    private void Awake()
    {
        _saveData = new SaveDataRepo();
        
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _uiController = FindObjectOfType<UIController>();
        _coinManager = FindObjectOfType<CoinManager>();

        _levelNumber = _saveData.LoadInt(SaveKeyManager.KeyLevelNumber);
        _coins = _saveData.LoadInt(SaveKeyManager.KeyCoins);
        _levelBuilder.BuildLevel(_levelNumber);
    }

    private void Start()
    {
        if (_levelNumber == 0) _levelNumber = 1;

        _coinManager.SetCurrentCoins(_coins);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        _levelNumber++;
        _coins = _coinManager.GetCurrentCoin();
        _coinManager.ResetCoin();

        SaveStats();

        RestartScene();
    }

    public void EndLevel(bool isLevelComplete)
    {
        _uiController.EndGame(isLevelComplete, _levelNumber);
        Time.timeScale = 0;
    }

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    private void SaveStats()
    {
        _saveData.SaveData(_levelNumber, SaveKeyManager.KeyLevelNumber);
        _saveData.SaveData(_coins, SaveKeyManager.KeyCoins);
    }
}