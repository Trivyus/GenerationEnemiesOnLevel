using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;

    public event Action<Enemy> MustBeReleased;

    private EnemyTarget _target;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetTarget(EnemyTarget target)
    {
        _target = target;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;

        _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * direction);

        transform.LookAt(_target.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyTarget>(out _))
            MustBeReleased?.Invoke(this);
    }
}