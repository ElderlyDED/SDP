using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] bool _gamePause;

    public void SetGamePause()
    {
        if (_gamePause == false)
        {
            _gamePause = true;
            Time.timeScale = 0f;
        }
        else
        {
            _gamePause = false;
            Time.timeScale = 1f;
        }
    }
}
