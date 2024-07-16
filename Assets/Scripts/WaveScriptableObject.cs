using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave",menuName = "Wave Manager/Wave",order =1)]
public class WaveScriptableObject : ScriptableObject
{
    public EnemyFormationScriptableObject[] enemyFormations;
    public float _waveDuration;
    public int _goldReward;
}
