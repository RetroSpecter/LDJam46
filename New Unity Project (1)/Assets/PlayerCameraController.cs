using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{

    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera ragdollCam;

    void Start()
    {
        GameManager.instance.PlayerFall += setRagdollCam;
        GameManager.instance.PlayerStand += setDefaultCam;
    }

    void setRagdollCam(GameObject g) {
        ragdollCam.Priority = 11;
    }

    void setDefaultCam()
    {
        ragdollCam.Priority = 9;
    }
}
