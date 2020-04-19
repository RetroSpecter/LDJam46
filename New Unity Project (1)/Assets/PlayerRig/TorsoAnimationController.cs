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

    private void Start()
    {
        GameManager.instance.OnFinishedCharging += TurnOnStep;
        OutletManager.instance.ChargingAtOutlet += turnOffStep;
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
        Debug.DrawRay(raycastPos, -Vector3.up * 10, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(raycastPos, -Vector3.up, out hit, 1000, ground))
        {
            home.position = hit.point;
        }
    }

    public void TurnOnStep(Outlet g) {
        on = true;
    }

    public void turnOffStep(Outlet g) {
        on = false;
    }

    bool on = true;
    IEnumerator LegUpdateCoroutine()
    {
        // Run continuously
        while (true)
        {
            if (!on) {
                yield return null;
                continue;
            }
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
