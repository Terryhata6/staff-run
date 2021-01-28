using UnityEngine;
using UnityEngine.SceneManagement;


public class MainController : MonoBehaviour
{
    [SerializeField] private int _levelNumber;

    private LevelBuilder _levelBuilder;
    private CoinManager _coinManager;
    private SaveDataRepo _saveData;
    private int _coins;


    private void Awake()
    {
        _saveData = new SaveDataRepo();
        
        _levelBuilder = FindObjectOfType<LevelBuilder>();

        _levelNumber = _saveData.LoadInt(SaveKeyManager.KeyLevelNumber);
        _coins = _saveData.LoadInt(SaveKeyManager.KeyCoins);
    }

    private void Start()
    {
        _levelBuilder.BuildLevel(_levelNumber);
    }

    private void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
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
        _coins += _coinManager.GetCurrentCoin();
        _coinManager.ResetCoin();

        SaveStats();

        RestartScene();
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