using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class TimeSetUI : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [SerializeField] TextMeshProUGUI _timeText;

    void Start()
    {
        SetTimeInUI();
    }

    void SetTimeInUI()
    {
        _gameStatus.Sec.Subscribe(v => {
            if(v < 10)
                _timeText.text = _gameStatus.Min.Value.ToString() + ":" + "0" + v.ToString();
            else
                _timeText.text = _gameStatus.Min.Value.ToString() + ":" + v.ToString();
        }).AddTo(this);
    }
}
