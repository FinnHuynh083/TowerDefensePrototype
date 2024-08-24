using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private LevelListManager _levelListManager;

    public void UpdateSelectedLevel()
    {
        _levelListManager.selectedLevelName = gameObject.name;
        print($"{_levelListManager.selectedLevelName}");
    }
}
