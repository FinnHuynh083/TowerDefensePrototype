using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform _aimCenter;
    [SerializeField] private float _aimRadius;
    [SerializeField] private LayerMask _enemyLayerMask;

    [SerializeField] private Targetter _targetter;

    //private Collider[] Enemies;

    //private void UpdateEnemyInRange()
    //{
    //    Enemies = Physics.OverlapSphere(_aimCenter.position, _aimRadius, _enemyLayerMask);

    //}
    private void LookAtCurrentEnemy()
    {

    }
}
