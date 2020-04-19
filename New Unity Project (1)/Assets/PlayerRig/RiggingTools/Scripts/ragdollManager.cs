using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollManager : MonoBehaviour
{
    Rigidbody[] jointRigid;
    Vector3[] jointPositions;
    Quaternion[] jointRotation;

    public GameObject ragdollRig;
    public GameObject baseRig;

    void Start()
    {
        jointRigid = GetComponentsInChildren<Rigidbody>();
        jointPositions = new Vector3[jointRigid.Length];
        jointRotation = new Quaternion[jointRigid.Length];
        for (int i = 0; i < jointRigid.Length; i++) {
            jointPositions[i] = jointRigid[i].transform.localPosition;
            jointRotation[i] = jointRigid[i].transform.localRotation;
        }
        resetRagdoll();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            resetRagdoll();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            turnOnRagdoll();
            jointRigid[0].AddForce(Vector3.back * 100, ForceMode.Impulse);
        }
    }
    */

    public void turnOnRagdoll() {
        SetIkRig(ragdollRig.transform, baseRig.transform);
        for (int i = 0; i < jointRigid.Length; i++)
        {
            jointRigid[i].isKinematic = false;
        }
    }

    public void addForceToRagdoll(Vector3 direction) {
        jointRigid[0].AddForce(direction, ForceMode.Impulse);
    }

    public void resetRagdoll() {
        for (int i = 0; i < jointRigid.Length; i++) {
            jointRigid[i].isKinematic = true;
        }

        SetIkRig(ragdollRig.transform, baseRig.transform);
    }

    void SetIkRig( Transform A, Transform B)
    {
        A.localPosition = B.localPosition;
        A.localRotation = B.localRotation;
        for (int i = 0; i < A.transform.childCount; i++) {
            Transform curA = A.transform.GetChild(i);
            Transform curB = B.transform.GetChild(i);
            SetIkRig(curA, curB);
        }
    }
}
