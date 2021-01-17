using UnityEngine;

public class UIController : MonoBehaviour
{
    private MainMenu _mainMenu;
    private InGameUI _inGameUI;
    private PauseMenu _pauseMenu;
    private EndGameMenu _endGameMenu;

    private void Start()
    {
        _mainMenu = GetComponentInChildren<MainMenu>();
        _inGameUI = GetComponentInChildren<InGameUI>();
        _pauseMenu = GetComponentInChildren<PauseMenu>();
        _endGameMenu = GetComponentInChildren<EndGameMenu>();
    }

    public void SwitchUI(UIState state)
    {
        switch (state)
        {
            case UIState.MainMenu:
                _mainMenu.Show();
                _inGameUI.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                break;
            case UIState.InGame:
                _mainMenu.Hide();
                _inGameUI.Show();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                break;
            case UIState.Pause:
                _mainMenu.Hide();
                _inGameUI.Hide();
                _pauseMenu.Show();
                _endGameMenu.Hide();
                break;
            case UIState.EndGame:
                _mainMenu.Hide();
                _inGameUI.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Show();
                break;
        }
    }

    public void StartGame()
    {
        //TODO
    }
    public void PauseGame()
    {
        //TODO
    }
    public void ResumeGame()
    {
        //TODO
    }
    public void EndGame()
    {
        //TODO
    }
    public void ExitGame()
    {
        //TODO
    }
}