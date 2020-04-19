using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControlScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cam;

    bool currentlyDown = false;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayerFall += Downed;
        GameManager.instance.PlayerStand += GetUp;
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyDown)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (Vector3.Distance(input, Vector3.zero) > 0.1f)
            {
                nav.isStopped = false;
                Debug.Log("in");
                input = cam.TransformDirection(input);
                input.y = 0;
                nav.SetDestination(transform.position + input.normalized * moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
                    input, Vector3.up), rotationSpeed * Time.deltaTime);
            }
            else
            {
                nav.isStopped = true;
            }
        }
    }

    private void Downed(GameObject o)
    {
        currentlyDown = true;
        // do other things?
    }

    private void GetUp()
    {
        currentlyDown = false;
        // do other things?
    }
}
