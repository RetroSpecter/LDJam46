using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{

    public GameObject player;
    public float camSpeed = 2f;
    public float camOffset = 2f;
    public float camVertOffset = 2f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = camSpeed * Time.deltaTime;
        offset = -player.transform.forward * camOffset + player.transform.up * camVertOffset;
        transform.position = Vector3.Lerp(transform.position, player.transform.localPosition + offset, interpolation);
        transform.LookAt(player.transform);
    }
}
