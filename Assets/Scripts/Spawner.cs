using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new (_spawnInterval);

        while (enabled)
        {
            SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Enemy enemy = Instantiate(spawnPoint.EnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemy.SetTarget(spawnPoint.Target);

            enemy.MustBeReleased += DestroyEnemy;

            yield return wait;
        }
    }
    private void DestroyEnemy(Enemy enemy)
    {
        enemy.MustBeReleased -= DestroyEnemy;
        Destroy(enemy.gameObject);
    }
}
