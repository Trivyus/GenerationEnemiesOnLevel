using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [field: SerializeField] public EnemyTarget Target { get; private set; }
    [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
}