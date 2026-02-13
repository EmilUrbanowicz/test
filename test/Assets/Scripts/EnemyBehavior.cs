using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform PatrolRoute;
    public List<Transform> Locations;
    private int _locationIndex = 0;
    public Transform Player;
    private int _lives = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        //2 
        if(other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
            _agent.destination = Player.position;
            Debug.Log("Enemy detected!");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        // 4
        if(other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        Player = GameObject.Find("Player").transform;
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;
        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    void Update()
    {
        if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Crtical hit!");
        }
    }

}
