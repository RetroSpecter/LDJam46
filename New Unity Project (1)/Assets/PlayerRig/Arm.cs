using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    Vector3 initialPos;
    GameObject initialParent;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        OutletManager.instance.ChargingAtOutlet += plugIntoOutlet;
        GameManager.instance.OnFinishedCharging += unplugFromOutlet;
        initialParent = transform.parent.gameObject;

    }


    void unplugFromOutlet(Outlet outlet) {
        StartCoroutine(unplugOutletCor(0.5f));
    }

    void plugIntoOutlet(Outlet outlet) {
        StartCoroutine(plugIntoOutletCor(outlet, 0.1f));
    }

    IEnumerator unplugOutletCor(float speed) {
        float t = 0;
        transform.parent = initialParent.transform;
        Vector3 start = transform.localPosition;
        Vector3 initialForward = transform.forward;
        while (t < speed) {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(start, initialPos, t/speed);
            transform.forward = Vector3.Lerp(initialForward,transform.parent.transform.forward, t/speed);
            yield return null;
        }
        transform.localPosition = initialPos;
        transform.forward = transform.parent.transform.forward;
    }

    IEnumerator plugIntoOutletCor(Outlet outlet, float speed)
    {
        float t = 0;
        transform.parent = outlet.transform;
        Vector3 start = transform.position;
        Vector3 initialForward = transform.forward;
        while (t < speed) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, outlet.transform.position, t / speed);
            transform.forward = Vector3.Lerp(initialForward, -outlet.transform.forward, t / speed);
            yield return null;
        }

        transform.position = outlet.transform.position;
        transform.forward = -outlet.transform.forward;
    }
}
