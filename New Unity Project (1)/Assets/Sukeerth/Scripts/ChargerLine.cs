using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ChargerLine : MonoBehaviour
{
    public Material lineMaterial;
    public int maxLineSegments = 100;
    public float endCapRigidity = 1f;
    private LineRenderer line;
    public Transform phonePort;
    public Transform port;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.material = lineMaterial;
        if (phonePort == null)
            phonePort = GameObject.FindGameObjectWithTag("Phone").transform.Find("Charger Connection");
        if (port == null)
            port = transform.Find("Charger Connection");
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePoints();
    }

    void CalculatePoints() {
        Vector3 point = port.position;
        Vector3[] positions = new Vector3[maxLineSegments + 4];
        positions[0] = port.position;
        positions[1] = port.position - transform.forward * endCapRigidity;
        for (int i = 2; i < maxLineSegments + 2; i++) {
            positions[i] = Vector3.Lerp(port.position, phonePort.position, i * (1.0f / (maxLineSegments + 2)));
        }
        positions[maxLineSegments + 2] = phonePort.position - Vector3.down * endCapRigidity;
        positions[maxLineSegments + 3] = phonePort.position;
        line.positionCount = maxLineSegments + 4;
        line.SetPositions(positions);
    }
}
