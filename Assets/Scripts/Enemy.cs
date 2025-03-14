using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Vector3 _direction;

    public event Action<Enemy> MustBeReleased;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyDestroyer enemyDestroer))
            MustBeReleased?.Invoke(this);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }
}