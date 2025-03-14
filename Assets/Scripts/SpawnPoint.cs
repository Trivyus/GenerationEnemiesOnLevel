using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = new(0, 0, 0);

    public Vector3 GetDirection() { return _direction; }
}
