using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutletManager : MonoBehaviour
{
    #region Singleton
    public static OutletManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    public int[] numOutletsNeededToPassStage;
    public Transform[] outletContainers;

    public event System.Action<Outlet> ChargingAtOutlet;

    private OutletContainer[] containers;
    private int currentOutletNum;
    public Outlet lastChargedOutlet;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnFinishedCharging += FinishedChargingAtOutlet;
        containers = new OutletContainer[outletContainers.Length];
        for (int i = 0; i < outletContainers.Length; i++) {
            containers[i] = new OutletContainer(outletContainers[i]);
        }
        containers[0].FlipBreakers(GameManager.instance.GetPlayerPosition());
    }

    public int GetOutletGoal() {
        return numOutletsNeededToPassStage[GetCurrentStage()];
    }

    public Outlet[] GetWorkingOutlets() {
        return containers[GetCurrentStage()].GetWorkingOutlets();
    }

    public void ChargeAtOutlet(Outlet outlet) {
        lastChargedOutlet = outlet;
        containers[GetCurrentStage()].FlipBreakers(GameManager.instance.GetPlayerPosition());
        ChargingAtOutlet?.Invoke(outlet);
    }

    public void FinishedChargingAtOutlet(Outlet outlet) {
        Debug.Log("Finished charging");
        currentOutletNum++;
        int currStage = GetCurrentStage();
        if (currentOutletNum >= numOutletsNeededToPassStage[currStage]) {
            GameManager.instance.ChangeStage(currStage + 1);
        }
        containers[GetCurrentStage()].FlipBreakers(GameManager.instance.GetPlayerPosition());
    }

    private int GetCurrentStage() {
        return (int)GameManager.instance.currentStage;
    }
}

[System.Serializable]
public struct OutletContainer
{
    public Outlet[] outlets;
    private Outlet[] brokenOutlets;
    private Outlet[] workingOutlets;

    public OutletContainer(Transform container) {
        outlets = container.GetComponentsInChildren<Outlet>();
        brokenOutlets = null;
        workingOutlets = null;
    }

    public void FlipBreakers(Vector3 playerPosition) {
        List<float> distances = new List<float>();
        workingOutlets = new Outlet[2];
        outlets = outlets.ToList().OrderByDescending(x => 
            Vector3.Distance(x.transform.position, playerPosition)).ToArray();
        workingOutlets = outlets.Take(2).ToArray();
        brokenOutlets = outlets.Skip(2).Take(outlets.Length - 2).ToArray();
        foreach (Outlet outlet in brokenOutlets) {
            outlet.triggerCollider.enabled = false;
            outlet.TurnOn(false);
        }
        foreach (Outlet outlet in workingOutlets) {
            if (outlet)
            {
                outlet.triggerCollider.enabled = true;
                outlet.TurnOn(true);
            }
        }
    }

    public Outlet[] ResetWorkingOutlets(Vector3 playerPosition) {
        FlipBreakers(playerPosition);
        return workingOutlets;
    }

    public Outlet[] GetWorkingOutlets() {
        return workingOutlets;
    }
}
