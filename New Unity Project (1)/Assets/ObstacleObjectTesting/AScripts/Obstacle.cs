using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour
{
    // Necessary fields
    public bool colliding;
    [SerializeField]
    public Waypoint[] points;
    public int current;
    public NavMeshAgent agent;
    public GameObject hitParticle;


    public virtual void Obstruct() {
        Debug.Log("You have been obstructed");
    }

    void Start () 
    {
        current = 0;
        if (points.Length == 0)
        {
            points = new Waypoint[] {new Waypoint(transform, 0, 0)};
        }
        points[current].setAgent(agent);
    }
    void Update () 
    {
        if (agent.remainingDistance == 0) {
            current++;
            if (current >= points.Length) {
                current = 0;
            }
            points[current].setAgent(agent);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerControlScript>() && !GameManager.instance.isInvulnerable) {
            AudioManager.instance.Play("Impact");
            Instantiate(hitParticle, collision.GetContact(0).point, Quaternion.identity);
            GameManager.instance.Colliding(this.gameObject);
        }
    }
}

[System.Serializable]
public struct Waypoint
{
    public Transform point;
    public float time;
    public float speed;
    public Waypoint(Transform point, float time, float speed) 
    {
        this.point = point;
        this.time = time;
        this.speed = speed;
    }

    public void setAgent(NavMeshAgent agent) {
        agent.SetDestination(point.position);
        agent.speed = speed;
    }
}
