using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProfilePresenter : MonoBehaviour
{
    [SerializeField] private PlayerProfileModel _playerProfileModel;
    [SerializeField] private TMP_Text _playerName;

    private void Start()
    {
        SetText();
    }

    public void SetText()
    {
        var playerName = _playerProfileModel._playerProfile.playerName;
        if (playerName != null)
        {
            _playerName.SetText(_playerProfileModel._playerProfile.playerName);
        }
    }
}
