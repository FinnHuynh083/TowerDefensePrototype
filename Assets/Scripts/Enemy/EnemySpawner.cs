using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _base;
    //spawn all wave

    public void StartSpawnWave(WaveScriptableObject wave) => StartCoroutine(SpawnWave(wave));
    //spawn formation - cua 1 wave
    private IEnumerator SpawnWave(WaveScriptableObject wave)
    {
        for (int i = 0; i < wave.enemyFormations.Length; i++)
        {
            yield return new WaitForSeconds(wave.enemyFormations[i]._formationDelaySpawnTime);
            //print("Return");
            StartCoroutine(SpawnFormation(wave.enemyFormations[i]));
        }
    }

    //spawn 1 formation - co the dung de spawn boss 
    private IEnumerator SpawnFormation(EnemyFormationScriptableObject enemyFormations)
    {
        SpawnEnemy(enemyFormations);
        for (int i = 1; i< enemyFormations._enemyCount; i++)
        {
            yield return new WaitForSeconds(enemyFormations._enemyDelaySpawnTime);
            SpawnEnemy(enemyFormations);
        }
    }

    private void SpawnEnemy(EnemyFormationScriptableObject enemyFormations)
    {
        var enemy = Instantiate(enemyFormations._enemyPrefab, transform.position, transform.rotation);
        if (enemy.TryGetComponent<EnemyMoving>(out EnemyMoving component))
        {
            component._speed = enemyFormations._speed;
            component._target = _base.transform;
        }
        if(enemy.TryGetComponent<EnemyAttack>(out EnemyAttack enemyAttack))
        {

            enemyAttack._target = _base;
        }
    }
    //spawn Boss
}