using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : MonoBehaviour
{
    public Collider triggerCollider;
    public GameObject sparksParticle;
    private OutletShaderController flashingShader;

    // Start is called before the first frame update
    void Start()
    {
        flashingShader = GetComponent<OutletShaderController>();
        if (triggerCollider == null) {
            triggerCollider = GetComponent<Collider>();
        }
        if (sparksParticle == null) {
            sparksParticle = Resources.Load("Sparks") as GameObject;
        }
    }

    public void TurnOn(bool on) {
        flashingShader.on = on;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("Charge");
            GameObject spark = Instantiate(sparksParticle, transform);
            OutletManager.instance.ChargeAtOutlet(this);
            Destroy(spark, 1);
            triggerCollider.enabled = false;
            flashingShader.on = false;
            if (!other.CompareTag("Player"))
                return;
            float dot = Vector3.Dot(other.transform.position - transform.position, transform.forward);
            if (dot < 0)
            {
                return;
            }
        }
    } 
}
