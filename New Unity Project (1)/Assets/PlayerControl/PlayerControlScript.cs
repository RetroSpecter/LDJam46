using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnObstacleCollide += Reaction;
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        float distance = moveSpeed * Time.deltaTime;
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xAxis, 0f, yAxis).normalized * distance;

        if (xAxis != 0)
        {
            playerBody.MoveRotation(Quaternion.Euler(0, playerBody.transform.eulerAngles.y + (xAxis * rotationSpeed), 0));
        }
        playerBody.MovePosition(transform.forward * movement.z + transform.right * movement.x + transform.position);
    }

    private void Reaction()
    {
        Debug.Log("I'm reacting!");
    }
}
