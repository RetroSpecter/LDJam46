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
        mps.data.offset = target.transform.localPosition/100; //Note: i had to do this because the scale was fucked on the model
        mrs.data.offset = target.transform.localEulerAngles;
    }
}
