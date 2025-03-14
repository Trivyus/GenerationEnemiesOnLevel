using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        InstantiatePool();    
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void InstantiatePool()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => GetFromPool(enemy),
            actionOnRelease: (enemy) => ReleaseInPool(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            Enemy enemy = _enemyPool.Get();
            SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            enemy.transform.position = spawnPoint.transform.position;
            enemy.SetDirection(spawnPoint.GetDirection());
            enemy.MustBeReleased += ReleaseEnemy;

            yield return wait;
        }
    }

    private void GetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void ReleaseInPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.MustBeReleased -= ReleaseEnemy;
        _enemyPool.Release(enemy);
    }
}
