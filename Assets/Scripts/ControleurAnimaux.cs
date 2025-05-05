using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControleurAnimaux : MonoBehaviour
{

    

    private Vector3 destination;
    private NavMeshAgent agent;

    [SerializeField]
    private float vie = 5f;
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
                    destination = new Vector3(Random.Range(-150f, 150f), 0.0f, Random.Range(-150f, 150f)); ;
                    agent.destination = destination;
                }
            }
            if (vie <= 0.0f)
        {
                Destroy(gameObject);
            }
        }
    
}
