using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TESTNavMeshPlayer : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(goal.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
