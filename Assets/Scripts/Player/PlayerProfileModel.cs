using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfileModel", menuName = "Player/PlayerProfileModel", order = 4)]
public class PlayerProfileModel : ScriptableObject
{
    public PlayerProfile _playerProfile;

    public string PlayerNameKey = "PlayerName";

    public string[] LevelKeys;

    public int BoolToInt(bool value)
    {
        if (value == true)
        {
            return 1;
        }
        if(value == false)
        {
            return 0;
        }
        return new int();
    }

    public bool IntToBool(int value)
    {
        if(value == 1)
        {
            return true;
        }
        if(value == 0)
        {
            return false;
        }
        return new bool();
    }

    public void SetPlayerName(string name)
    {
        _playerProfile.playerName = name;
    }

    //doi cac sting thanh array/dictionary
    public void Save()
    {
        PlayerPrefs.SetString(PlayerNameKey, _playerProfile.playerName);
        //PlayerPrefs.SetInt(Level1_2Key, BoolToInt(_playerProfile.LevelDictionary[Level1_2Key]));
        foreach(var item in LevelKeys)
        {
            int value = BoolToInt(_playerProfile.LevelDictionary[item]);
            PlayerPrefs.SetInt(item, value);
        }
    }

    public void Load()
    {
        _playerProfile.playerName = PlayerPrefs.GetString(PlayerNameKey);
        //_playerProfile.level1_2 = IntToBool(PlayerPrefs.GetInt(Level1_2Key));
        foreach (var item in LevelKeys)
        {
            _playerProfile.LevelDictionary[item] = IntToBool(PlayerPrefs.GetInt(item));
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        _playerProfile.playerName = null;
    }
}
