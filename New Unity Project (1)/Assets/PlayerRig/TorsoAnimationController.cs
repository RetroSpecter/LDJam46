using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoAnimationController : MonoBehaviour
{

    [SerializeField] LegStepper leftLeg;
    [SerializeField] LegStepper rightLeg;
    [SerializeField] Transform leftHomePosition;
    [SerializeField] Transform rightHomePosition;
    [SerializeField]  LayerMask ground;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

    private void Update()
    {
        FloorTransform(leftHomePosition);
        FloorTransform(rightHomePosition);
    }

    public void FloorTransform(Transform home)
    {
        Vector3 raycastPos = home.position;
        raycastPos.y = transform.position.y;
        Debug.DrawRay(raycastPos, -transform.up * 10, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(raycastPos, -transform.up, out hit, 1000, ground))
        {
            home.position = hit.point;
        }
    }

    IEnumerator LegUpdateCoroutine()
    {
        // Run continuously
        while (true)
        {
            // Try moving one diagonal pair of legs
            do
            {
                leftLeg.TryMove();
                yield return null;
            } while (leftLeg.Moving);

            do
            {
                rightLeg.TryMove();
                yield return null;
            } while (rightLeg.Moving);
        }
    }

}
