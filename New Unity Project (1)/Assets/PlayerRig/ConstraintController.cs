using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ConstraintController : MonoBehaviour
{

    MultiPositionConstraint mps;
    MultiRotationConstraint mrs;
    public Transform target;

    void Start()
    {
        mps = GetComponent<MultiPositionConstraint>();
        mrs = GetComponent<MultiRotationConstraint>();
    }

    void Update()
    {
        mps.data.offset = target.transform.localPosition;
        mrs.data.offset = target.transform.localEulerAngles;
    }
}
