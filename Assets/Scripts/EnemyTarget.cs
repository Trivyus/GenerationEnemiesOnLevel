using System;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float speed = 3f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        MoveToWaypoints();
    }

    private void MoveToWaypoints()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }
}