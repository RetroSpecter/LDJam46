using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BlendRiggingController : MonoBehaviour
{
    [Range(0, 1)]
    //TODO: doing both position adn rotation in one but can do them separate if requred
    public float globalBlend;

    void Update()
    {
        UpdateBlend(transform.GetChild(0));
    }

    void UpdateBlend(Transform curBlend)
    {
        BlendConstraint constraint = curBlend.GetComponent<BlendConstraint>();
        constraint.data.positionWeight = globalBlend;
        constraint.data.rotationWeight = globalBlend;
        for (int i = 0; i < curBlend.childCount; i++)
        {
            UpdateBlend(curBlend.GetChild(i));
        }
    }
}
