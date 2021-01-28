using UnityEngine;
using UnityEngine.SceneManagement;


public class MainController : MonoBehaviour
{
    [SerializeField] private int _levelNumber;

    private LevelBuilder _levelBuilder;
    private SaveDataRepo _saveData;
    private int _currentCoins;
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
        
    }
    public void ResumeGame()
    {

    }
    public void NextLevel()
    {
        _levelNumber++;
        _coins += 123456; // TODO
        SaveStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveStats()
    {
        _saveData.SaveData(_levelNumber, SaveKeyManager.KeyLevelNumber);
        
    }
}