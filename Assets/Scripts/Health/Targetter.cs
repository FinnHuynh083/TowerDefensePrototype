using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    [SerializeField] private float _targetRange;
    [SerializeField] private SphereCollider _unitSphereCollider;
    [SerializeField] private float _scanRate=0.1f;
    [SerializeField] private Transform _basePos;

    private List<Targetable> InRangeEnemies = new List<Targetable>();
    private Targetable _currentTarget= null;
    private float _scanTimer;
    /// <summary>
    /// Current Target Who in range & being looked
    /// </summary>
    public Targetable CurrentTarget => _currentTarget;

    private void Start()
    {
        _unitSphereCollider.radius = _targetRange;
        _scanTimer = _scanRate;
    }
    private void Update()
    {
        UpdateTarget();
        //PrintCurrentTarget();
    }

    private void PrintCurrentTarget()
    {
        if (_currentTarget != null)
        {
            print($"Current Target:{_currentTarget.name}");
        }
    }

    private void UpdateTarget()
    {
        //can nhac nhung courtine hoac invoke repeating
        //quai in range
        //quai nao gan base nhat thi ban quai do
        _scanTimer -= Time.deltaTime;
        if (_scanTimer <= 0)
        {
            _currentTarget = FindTargetNearestBase();
            if (_currentTarget != null)
            {
                transform.LookAt(_currentTarget.transform);
            }
            _scanTimer = _scanRate;
        }
        //them dk luc quai die
        if (_currentTarget!=null&& _currentTarget.IsDead)
        {
            //remove khoi list
            InRangeEnemies.Remove(_currentTarget);
            //tim lai enemy gan nhat
            _currentTarget = FindTargetNearestBase();
        }
    }
    private void PrintInRangeEnemies(List<Targetable> targetables)
    {
        print("Enemies In Range");
        foreach (var i in targetables)
        {
            print($"{i.gameObject.name}");
        }
    }

    /// <summary>
    /// Add in range enemy in list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Targetable>(out Targetable inRangeTarget))
        {
            InRangeEnemies.Add(inRangeTarget);
        };
    }
    /// <summary>
    /// Remove out of range enemy in list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Targetable>(out Targetable outRangeTarget))
        {
            InRangeEnemies.Remove(outRangeTarget);
        };
    }

    /// <summary>
    /// Find Nearest target
    /// </summary>
    /// <returns></returns>
    private Targetable FindTargetNearestBase()
    {
        Targetable result=null;

        //if (InRangeEnemies.Count <= 0) return result;

        float minDistance = float.MaxValue;
        for (int i = 0; i < InRangeEnemies.Count; i++)
        {
            var distance = Vector3.Distance(InRangeEnemies[i].Position, _basePos.position);
            if (distance < minDistance)
            {
                //remaining distance dang tra ve infinite
                //tinh remaining distance =tay - theo duong chim bay
                minDistance = distance;
                result = InRangeEnemies[i];
            }
        }
        return result;
    }

}
