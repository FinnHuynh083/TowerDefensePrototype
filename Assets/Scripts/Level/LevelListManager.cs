using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class LevelListObject
{
    public GameObject[] objects;
    //[SerializeField]public Dictionary<string, GameObject> ObjectDictionary = new Dictionary<string, GameObject>();

}
public class LevelListManager : MonoBehaviour
{
    [SerializeField] private LevelListObject _levelListObject;
    [SerializeField] PlayerProfileModel _playerProfileModel;
    [NonSerialized]public string selectedLevelName;
    public UnityEvent<string> levelStart;

    private void Start()
    {
        ShowAvailableLevel();
    }
    public void StartLevel()
    {
        if (selectedLevelName != null)
        {
            levelStart.Invoke(selectedLevelName);
        }
    }

    public void ShowAvailableLevel()
    {
        var playerProfile= _playerProfileModel._playerProfile;
        foreach(var item in _levelListObject.objects)
        {
            if (playerProfile.LevelDictionary[item.name] == true)
            {
                item.SetActive(true);
            }
        }
    }
}
