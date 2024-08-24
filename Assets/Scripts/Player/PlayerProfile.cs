using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProfile
{
    public string playerName;
    public Dictionary<string, bool> LevelDictionary = new Dictionary<string, bool>
    {
        {"Level1.2",false},
        {"Level1.3",false},
        {"Level1.4",false},
        {"Level1.5",false},

        {"Level2.1",false},
        {"Level2.2",false},
        {"Level2.3",false},
        {"Level2.4",false},
        {"Level2.5",false},
    };

    public void ClearPlayerProfile()
    {
        foreach(var item in LevelDictionary)
        {
            LevelDictionary[item.Key] = false;
        }

        playerName = null;
    }
    public void SetBoolTrue(string levelName)
    {
        //LevelDictionary.ContainsKey(levelName);
        LevelDictionary[levelName] = true;
    }
}
