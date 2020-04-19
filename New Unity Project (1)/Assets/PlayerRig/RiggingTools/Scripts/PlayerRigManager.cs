using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigManager : MonoBehaviour
{

    public ragdollManager ragdollManager;
    public BlendRiggingController blendRiggingController;

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchToRig();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchToRagdoll();
        }
    }
    */
    

    public void SwitchToRagdoll() {
        blendRiggingController.globalBlend = 1;
        ragdollManager.turnOnRagdoll();
        ragdollManager.addForceToRagdoll();
    }

    public void SwitchToRig() {
        StartCoroutine(switchToRigEnum(0.5f));
    }

    IEnumerator switchToRigEnum(float time) {
        float curTime = 0;
        while (curTime < time)
        {
            blendRiggingController.globalBlend = Mathf.Lerp(1, 0, curTime/time);
            curTime += Time.deltaTime;
            yield return null;
        }
        blendRiggingController.globalBlend = 0;
        ragdollManager.resetRagdoll();
    }
}
