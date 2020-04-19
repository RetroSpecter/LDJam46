using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        OutletManager.instance.ChargingAtOutlet += plugIntoOutlet;
        GameManager.instance.OnFinishedCharging += unplugFromOutlet;
    }


    void unplugFromOutlet(Outlet outlet) {
        transform.localPosition = initialPos;
        transform.forward = transform.parent.transform.forward;
    }

    void plugIntoOutlet(Outlet outlet) {
        transform.position = outlet.transform.position;
        transform.forward = -outlet.transform.forward;
    }
}
