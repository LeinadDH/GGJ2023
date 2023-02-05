using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, iFlashLightSensitive
{
    [Header("Debugging Variables")] 
    [SerializeField]private bool debugging;
    [Header("Serialized References")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> wayPoints;
    
    [Header("Configurable Variables")]
    [SerializeField] private float aggroRange;
    [SerializeField] private float deAggroRange;
    [Tooltip("Rate at which the enemy is slowed ")]
    [SerializeField] private float stunRate;
    [Tooltip("Rate at which the enemy recovers from slow ")]
    [SerializeField] private float stunRecoveryRate;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float pursuitSpeed;
    
    private NavMeshAgent _agent;
    private int _currentWayPoint;
    private bool _pursue;
    private bool _inLight;
    void Awake()
    {
        Prepare();
    }
    
    void Update()
    {
        EnemyStates();
        CheckPlayerDistance();
        Logging();
    }
    
    private void EnemyStates()
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
        _agent.SetDestination(wayPoints[_currentWayPoint].transform.position);
        CycleWayPoints();
    }

    private void CycleWayPoints()
    {
        if (transform.position.x == wayPoints[_currentWayPoint].transform.position.x 
            && transform.position.z == wayPoints[_currentWayPoint].transform.position.z)
        {
            if (_currentWayPoint == wayPoints.Count-1)
            {
                _currentWayPoint = 0;
                return;
            }
            _currentWayPoint++;
        }
    }
    
    private void CheckPlayerDistance()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        
        switch (playerDistance)
        {
            case float n when(n < aggroRange):
                CheckLineOfSight();
                break;
            
                case float n when(n < deAggroRange):
                Debug.DrawLine(transform.position, player.transform.position,Color.green);
                break;
                
            case float n when(n >= deAggroRange):
                _pursue = false;
                break; 
        }
    }

    private void CheckLineOfSight()
    {
        Vector3 dirToPlayer = player.transform.position - transform.position;
        if(!Physics.Raycast(transform.position, dirToPlayer.normalized,out RaycastHit hit, deAggroRange))return;
        if (!hit.collider.CompareTag("Player")) return;
        Debug.DrawRay(transform.position, dirToPlayer,Color.red);
        _pursue = true;

    }
    
    #region Debugging And Visualizing

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, aggroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, deAggroRange);
    }

    void Logging()
    {
        Debug.Log(_inLight);
        
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("FlashLight"))return;
        OnFlashLightHit();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("FlashLight")) return; 
        OnFlashLightExit();
    }

    public void OnFlashLightHit()
    {
        if (!_inLight) return;
        if (_agent.speed <= 0)
        {
            _agent.speed = 0;
            return;
        }
        _agent.speed -= stunRate * Time.fixedDeltaTime;
    }

    public void OnFlashLightExit()
    {
        switch (_pursue)
        {
            case true:
                if (_agent.speed < pursuitSpeed)
                {
                    _agent.speed = pursuitSpeed;
                }
                break;
            case false:
                if (_agent.speed < patrolSpeed)
                {
                    _agent.speed = patrolSpeed;
                }   
                break;
        }
        _agent.speed += stunRecoveryRate;
    }

    private void Prepare()
    {
        Application.targetFrameRate = 60;
        try
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        catch { Debug.LogWarning("Could not find NavMeshAgent"); }

        debugging = false;
    }
}
