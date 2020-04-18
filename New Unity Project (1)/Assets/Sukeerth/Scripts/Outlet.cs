using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : MonoBehaviour
{
    public Collider triggerCollider;
    // Start is called before the first frame update
    void Start()
    {
        if (triggerCollider == null) {
            triggerCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            OutletManager.instance.ChargeAtOutlet();
        }
    }
}
