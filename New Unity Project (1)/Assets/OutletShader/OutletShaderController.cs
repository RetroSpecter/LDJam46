using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutletShaderController : MonoBehaviour
{

    private Material mat;
    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetInt("OnOff", on ? 1 : 0);
    }
}
