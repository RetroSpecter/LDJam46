using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : MonoBehaviour
{
    public Collider triggerCollider;
    public GameObject sparksParticle;
    // Start is called before the first frame update
    void Start()
    {
        if (triggerCollider == null) {
            triggerCollider = GetComponent<Collider>();
        }
        if (sparksParticle == null) {
            sparksParticle = Resources.Load("Sparks") as GameObject;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameObject spark = Instantiate(sparksParticle, transform);
            OutletManager.instance.ChargeAtOutlet(this);
            Destroy(spark, 1);
            triggerCollider.enabled = false;
        }
    } 
}
