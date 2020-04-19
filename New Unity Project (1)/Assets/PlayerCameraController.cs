using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{

    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera ragdollCam;
    public CinemachineVirtualCamera chargingCam;

    void Start()
    {
        GameManager.instance.PlayerFall += setRagdollCam;
        GameManager.instance.PlayerStand += setDefaultCam;
        GameManager.instance.OnFinishedCharging += setDefaultCam;
        OutletManager.instance.ChargingAtOutlet += setChargingCam;
    }

    void setRagdollCam(GameObject g) {
        ragdollCam.Priority = 11;
    }

    void setDefaultCam()
    {
        ragdollCam.Priority = 9;

    }

    void setDefaultCam(Outlet g)
    {
        chargingCam.Priority = 9;
    }

    void setChargingCam(Outlet g)
    {
        chargingCam.Priority = 11;
    }
}
