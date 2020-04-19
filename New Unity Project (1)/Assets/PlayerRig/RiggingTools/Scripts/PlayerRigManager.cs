using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigManager : MonoBehaviour
{

    public ragdollManager ragdollManager;
    public BlendRiggingController blendRiggingController;

    private void Start()
    {
        GameManager.instance.PlayerFall += SwitchToRagdoll;
        GameManager.instance.PlayerStand += SwitchToRig;
    }

    public void SwitchToRagdoll(GameObject obstacle) {
        GameManager.instance.PlayerFall -= SwitchToRagdoll;
        blendRiggingController.globalBlend = 1;
        ragdollManager.turnOnRagdoll();


        Vector3 direction = obstacle.transform.position - transform.position;
        direction.y = 0;

        ragdollManager.addForceToRagdoll(direction * 50 + Vector3.up * 10);
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
        GameManager.instance.PlayerFall += SwitchToRagdoll;
    }
}
