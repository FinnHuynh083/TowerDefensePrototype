using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gold : MonoBehaviour
{
    [SerializeField] private int _GoldStart;
    public UnityEvent<int> BudgetChanged;

    private int _currentBudget=0;

    private void Start()
    {
        ChangeBudget(_GoldStart);
    }

    public void ChangeBudget(int value)
    {
        _currentBudget += value;
        if (_currentBudget <0)
        {
            _currentBudget = 0;
        }
        BudgetChanged.Invoke(_currentBudget);
    }

    public bool IsEnough(int requiredGold)
    {
        if(_currentBudget >= requiredGold)
        {
            return true;
        }
        return false;
    }
}
