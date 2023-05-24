using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [Inject] PlayerInput _playerInput;
    [SerializeField] Animator _menuAnimator;
    [SerializeField] bool _activMenu;

    void OnEnable() => _playerInput.pauseAction += StartPause;

    void OnDisable() => _playerInput.pauseAction -= StartPause;

    public void StartPause()
    {
        if (!_activMenu)
        {
            _gameStatus.SetGamePause();
            _activMenu = true;
            _menuAnimator.SetBool("IsUp", true);
            _menuAnimator.SetBool("IsDown", false);
        }
        else
        {
            _gameStatus.SetGamePause();
            _activMenu = false;
            _menuAnimator.SetBool("IsDown", true);
            _menuAnimator.SetBool("IsUp", false);
        }

    }

    public void MainMenuBtn() 
    {
        _gameStatus.SetGamePause();
        SceneManager.LoadScene(0);
    }
}
