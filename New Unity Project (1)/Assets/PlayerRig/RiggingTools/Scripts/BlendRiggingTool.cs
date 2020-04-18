using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BlendRiggingTool : MonoBehaviour
{


    public GameObject reference;
    public GameObject sourceA;
    public GameObject sourceB;

    public void BlendRigCharacter() {
        DeleteChildren();
        BlendRigBone(reference.transform, sourceA.transform, sourceB.transform, this.transform);
    }

    void DeleteChildren() {
        for (int i = 0; i < transform.childCount; i++) {
            DestroyImmediate(this.transform.GetChild(i).gameObject);
        }
    }

    void BlendRigBone(Transform currentReference, Transform A, Transform B, Transform current) {
        for (int i = 0; i < currentReference.transform.childCount; i++) {
            Transform curBone = currentReference.transform.GetChild(i);
            Transform curA = A.transform.GetChild(i);
            Transform curB = B.transform.GetChild(i);
            GameObject newBlend = new GameObject("blend:" + curBone.name);
            newBlend.transform.parent = current;

            BlendConstraint newConstraint = newBlend.AddComponent<BlendConstraint>();
            newConstraint.data.constrainedObject = curBone;
            newConstraint.data.sourceObjectA = curA;
            newConstraint.data.sourceObjectB = curB;

            BlendRigBone(curBone, curA, curB,newBlend.transform);
        }
    }
}
