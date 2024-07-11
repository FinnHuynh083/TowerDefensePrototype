using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private int _readyTime=10;

    public WaveScriptableObject[] waves;
    public UnityEvent<WaveScriptableObject> WaveStart;

    private int _currentWave=0;

    private float _currentWaveTime;
    private int CurrentWaveTime => Mathf.RoundToInt(_currentWaveTime);
    private int _lastWaveTime = -1;
    private int _lastWave = -1;
    private bool IsReady=true;

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        CountDown();
        UpdateWave();
        if (_lastWaveTime != CurrentWaveTime)
        {
            _timerText.SetText(CurrentWaveTime.ToString());

            _lastWaveTime = CurrentWaveTime;
        }
        if (_lastWave != _currentWave)
        {
            _waveText.SetText($"Wave: {_currentWave+1}");
            _lastWave = _currentWave;
        }
    }

    private void StartLevel()
    {
        WaveStart.Invoke(waves[_currentWave]);
        _currentWaveTime = waves[_currentWave]._waveDuration;
    }

    private void UpdateWave()
    {
        if (_currentWaveTime <= 0&&IsReady== true)
        {
            if (_currentWave >= waves.Length-1)
            {
                return;
            }
            StartCoroutine("ReadyForNextWave");

        }
    }

    private IEnumerator ReadyForNextWave()
    {
        _currentWaveTime = _readyTime;
        IsReady = false;
        yield return new WaitForSeconds(_readyTime);

        _currentWave += 1;
        _currentWaveTime = waves[_currentWave]._waveDuration;
        WaveStart.Invoke(waves[_currentWave]);
        IsReady = true;

    }

    private float CountDown()
    {
        _currentWaveTime -= Time.deltaTime;
        if (_currentWaveTime < 0)
        {
            _currentWaveTime = 0;
        }
        return _currentWaveTime;
    }
}
