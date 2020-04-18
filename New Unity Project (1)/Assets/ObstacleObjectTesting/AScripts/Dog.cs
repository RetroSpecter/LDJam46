using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : Obstacle
{
    public Waypoint player;
    public float followRange;
    void Start()
    {
        agent.SetDestination(player.point.position);
        if (agent.remainingDistance <= followRange) 
        {
            agent.speed = player.speed;
        }
        else 
        {
            agent.speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.point.position);
        if (agent.remainingDistance <= followRange)
        {
            agent.speed = player.speed;
        }
        else
        { 
            agent.SetDestination(points[current].point.position);
            if (agent.remainingDistance == 0) 
            {
                current++;
                if (current >= points.Length) 
                {
                    current = 0;
                }
                points[current].setAgent(agent);
            }
        }
    }
}
