using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [field: SerializeField] public bool GamePause {  get; private set; }

    [field: SerializeField] public BoolReactiveProperty GameEnded { get; private set; } = new();

    [SerializeField] IntReactiveProperty _allSec;
    [field: SerializeField] public IntReactiveProperty Min { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty Sec { get; private set; } = new();

    [SerializeField] AudioSource _audioSource;

    void Start()
    {
        Timer();
        SetMin();
    }

    void Timer() => Observable.Timer(TimeSpan.FromSeconds(1f)).Repeat().Subscribe(v => {
        _allSec.Value++;
        Sec.Value++;
    }).AddTo(this);

    void SetMin() => Sec.Where(s => s >= 60).Subscribe(v => {
        Sec.Value = 0;
        Min.Value++;
    }).AddTo(this);
    

    public void SetGamePause()
    {
        if (GamePause == false)
        {
            GamePause = true;
            Time.timeScale = 0f;
        }
        else
        {
            GamePause = false;
            Time.timeScale = 1f;
        }
    }

    public void EndGame()
    {
        _audioSource.Play();
        SetGamePause();
        GameEnded.Value = true;
    }
}
