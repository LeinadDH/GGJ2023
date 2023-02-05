using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private List<Vector3> wayPoints;
    [SerializeField] private GameObject player;
    private NavMeshAgent _agent;
    private bool _pursue = false;
    private int _currentWayPoint = 0;
    void Awake()
    {
        Prepare();
    }

    void Update()
    {
        switch (_pursue)
        {
            case false:
                FollowWayPoints();
                break;
            case true:
                _agent.SetDestination(player.transform.position);
                break;
        }
    }

    private void FollowWayPoints()
    {
        _agent.SetDestination(wayPoints[_currentWayPoint]);
    }

    private void Prepare()
    {
        try
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        catch { Debug.LogWarning("Could not find NavMeshAgent"); }
    }
}
