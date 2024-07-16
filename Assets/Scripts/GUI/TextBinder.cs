using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBinder : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _goldText;

    public void SetWaveText(int _wave) => _waveText.SetText($"Wave: {_wave}");

    public void SetTimerText(int _time) => _timerText.SetText($"Time: {_time}");

    public void SetGoldText(int _gold) => _goldText.SetText($"Gold: {_gold}");

}
