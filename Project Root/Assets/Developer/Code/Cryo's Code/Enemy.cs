using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, iFlashLightSensitive
{
    [Header("Debugging Variables")] 
    [Header("Serialized References")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected List<GameObject> wayPoints;
    
    [Header("Configurable Variables")]
    [SerializeField] protected float aggroRange = 5f;
    [SerializeField] protected float deAggroRange = 10f;
    [Tooltip("Rate at which the enemy is slowed ")]
    [SerializeField] protected float stunRate = 10f;
    [Tooltip("Rate at which the enemy recovers from slow ")]
    [SerializeField] protected float stunRecoveryRate = 10f;

    [SerializeField] protected float patrolSpeed = 3.5f;
    [SerializeField] protected float pursuitSpeed = 10f;
    
    protected NavMeshAgent _agent;
    protected int _currentWayPoint;
    protected bool _pursue;
    protected bool _inLight;
    void Awake()
    {
        Prepare();
    }
    
    protected virtual void Update()
    {
        EnemyStates();
        CheckPlayerDistance();
    }
    
    protected void EnemyStates()
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
    
    protected void FollowWayPoints()
    {
        _agent.SetDestination(wayPoints[_currentWayPoint].transform.position);
        CycleWayPoints();
    }

    protected void CycleWayPoints()
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
    
    protected void CheckPlayerDistance()
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

    protected void CheckLineOfSight()
    {
        Vector3 dirToPlayer = player.transform.position - transform.position;
        if(!Physics.Raycast(transform.position, dirToPlayer.normalized,out RaycastHit hit, deAggroRange))return;
        if (!hit.collider.CompareTag("Player")) return;
        Debug.DrawRay(transform.position, dirToPlayer,Color.red);
        _pursue = true;

    }
    
    #region Debugging And Visualizing

    protected void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, aggroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, deAggroRange);
    }

    #endregion

    protected void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("FlashLight"))return;
        OnFlashLightHit();
    }
    
    protected void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("FlashLight")) return; 
        OnFlashLightExit();
    }

    public void OnFlashLightHit()
    {
        _inLight = true;
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
        _inLight = false;
        switch (_pursue)
        {
            case true:
                if (_agent.speed > pursuitSpeed)
                {
                    _agent.speed = pursuitSpeed;
                }
                break;
            case false:
                if (_agent.speed > patrolSpeed)
                {
                    _agent.speed = patrolSpeed;
                }   
                break;
        }
                _agent.speed += stunRecoveryRate * Time.fixedDeltaTime;
    }

    protected void Prepare()
    {
        Application.targetFrameRate = 60;
        try
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        catch { Debug.LogWarning("Could not find NavMeshAgent"); }

    }
}
