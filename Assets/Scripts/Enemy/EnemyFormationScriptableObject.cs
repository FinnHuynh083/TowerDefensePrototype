using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyFormation",menuName ="Enemy/Enemy Formation",order =2)]
public class EnemyFormationScriptableObject : ScriptableObject
{
    [SerializeField] public GameObject _enemyPrefab;
    [SerializeField] public float _speed;
    [SerializeField] public int _enemyCount;
    [SerializeField] public float _enemyDelaySpawnTime;
    [SerializeField] public float _formationDelaySpawnTime;
}
