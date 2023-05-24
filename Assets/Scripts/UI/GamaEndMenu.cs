using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GamaEndMenu : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [SerializeField] Animator _animator;
    [SerializeField] TextMeshProUGUI _timeText;
    void Start()
    {
        _gameStatus.GameEnded.Where(v => v == true).Subscribe(v => {
            _animator.SetBool("IsUp", true);
            if (_gameStatus.Sec.Value < 10)
                _timeText.text = _gameStatus.Min.Value.ToString() + ":" + "0" + _gameStatus.Sec.Value.ToString();
            else
                _timeText.text = _gameStatus.Min.Value.ToString() + ":" + _gameStatus.Sec.Value.ToString();
        }).AddTo(this);
    }

    public void MainMenuBtn()
    {
        _gameStatus.SetGamePause();
        SceneManager.LoadScene(0);
    }

    
}
