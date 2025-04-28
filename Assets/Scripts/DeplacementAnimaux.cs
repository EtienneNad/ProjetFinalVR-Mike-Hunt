using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeplacementAnimaux : MonoBehaviour
{

    

    private Vector3 destination;
    private NavMeshAgent agent;

  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

  

    private Vector3 GenererDestinationAlea()
    {

        return new Vector3(Random.Range(-150, 150f), 0.0f, Random.Range(-150f, 150f));
    }

   

        // Update is called once per frame
        void Update()
        {
            if (!agent.pathPending)
            {
                float distanceDestination = Vector3.SqrMagnitude(destination - transform.position);
                if (Mathf.Approximately(agent.velocity.sqrMagnitude, 0.0f) ||
                    Mathf.Approximately(distanceDestination, 0.0f) || !agent.hasPath)
                {
                    destination = GenererDestinationAlea();
                    agent.destination = destination;
                }
            }
        }
    
}
