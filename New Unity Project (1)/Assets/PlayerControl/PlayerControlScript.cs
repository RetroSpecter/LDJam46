﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControlScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cam;

    bool currentlyDown = false;
    private NavMeshAgent nav;
    public GameObject getUpCenter;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayerFall += Downed;
        GameManager.instance.PlayerStand += GetUp;
        nav = transform.parent.GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyDown)
        {
            float input = Input.GetAxisRaw("Vertical");
            transform.forward = new Vector3(cam.forward.x, 0, cam.forward.z);
            Vector3 destination = transform.position + transform.forward *
                input * moveSpeed * Time.deltaTime;
            nav.SetDestination(destination);
        }
    }

    private void Downed(GameObject o)
    {
        GameManager.instance.isInvulnerable = true;
        currentlyDown = true;
        AudioManager.instance.PlayRandomHurt();
        // do other things?
    }

    private void GetUp()
    {
        GameManager.instance.isInvulnerable = false;
        AudioManager.instance.PlayRandomRecovery();
        currentlyDown = false;
        Vector3 targetPosition = getUpCenter.transform.position;
        targetPosition.y = transform.position.y;
        nav.Warp(targetPosition);
        // do other things?
    }
}
