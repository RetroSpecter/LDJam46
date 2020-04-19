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
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyDown)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            input = cam.transform.TransformDirection(input);
            input.y = 0;
            Vector3 destination = transform.position + input.normalized * moveSpeed * Time.deltaTime;
            //if (input != Vector3.zero) {
            //    transform.rotation = Quaternion.Slerp(transform.rotation, 
            //        Quaternion.LookRotation(input.normalized), rotationSpeed * Time.deltaTime);
            //}
            nav.SetDestination(destination);
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
