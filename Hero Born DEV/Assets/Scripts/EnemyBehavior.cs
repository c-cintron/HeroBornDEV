using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    private bool followMode;

    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private GameBehavior _gameManager;

    private int _lives = 3;
    public int EnemyLives
    {
        get {return _lives; }

        set
        {
        _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();

        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
        if (followMode)
        {
            ImprovedAI();
        }
    }

    void ImprovedAI()
    {
        agent.destination = player.position;
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            followMode = true;
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            followMode = false;
            Debug.Log("Player out of range, resume patrol.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            _gameManager.EnemyLives -= 1;
            EnemyLives -= 1;
            Debug.Log("That man has a family! D:");
        }
    }
}
