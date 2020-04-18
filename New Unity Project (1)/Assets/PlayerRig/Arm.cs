using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    public GameObject testTarget;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;    
    }

    // Update is called once per frame
    void Update()
 {
        if (Input.GetKeyDown(KeyCode.P))
        {
            unplugFromOutlet();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            plugIntoOutlet(testTarget);
        }
    }

    void unplugFromOutlet() {
        transform.localPosition = initialPos;
        transform.forward = transform.parent.transform.forward;
    }

    void plugIntoOutlet(GameObject outlet) {
        transform.position = outlet.transform.position;
        transform.forward = -outlet.transform.forward;
    }
}
