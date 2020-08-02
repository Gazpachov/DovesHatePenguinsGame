using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] steepGoals;
    private int tarPoint;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        tarPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        NextSteep();
    }
    void NextSteep()
    {
        if (steepGoals.Length == 0)
        {
            return;
        }
        agent.destination = steepGoals[tarPoint].position;
        tarPoint = (tarPoint + 1) % steepGoals.Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.pathPending )
        {
            NextSteep();
        }
        
        
    }
}
