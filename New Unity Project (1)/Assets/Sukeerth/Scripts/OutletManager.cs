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

    public event System.Action ChargingAtOutlet;

    private OutletContainer[] containers;

    // Start is called before the first frame update
    void Start()
    {
        containers = new OutletContainer[outletContainers.Length];
        for (int i = 0; i < outletContainers.Length; i++) {
            containers[i] = new OutletContainer(outletContainers[i]);
        }
        containers[0].FlipBreakers(GameManager.instance.GetPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetOutletGoal() {
        return numOutletsNeededToPassStage[GetCurrentStage()];
    }

    public Outlet[] GetWorkingOutlets() {
        return containers[GetCurrentStage()].GetWorkingOutlets();
    }

    public void ChargeAtOutlet() {
        ChargingAtOutlet?.Invoke();
    }

    public void FinishedChargingAtOutlet() {
        // TODO: handle stage transitions
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
        float[] maxDist = new float[2];
        workingOutlets = new Outlet[2];
        List<Outlet> notWorkingOutlets = new List<Outlet>();
        for (int i = 0; i < outlets.Length; i++) {
            float dist = Vector3.Distance(outlets[i].transform.position, playerPosition);
            if (dist > maxDist[0]) {
                maxDist[0] = dist;
                workingOutlets[0] = outlets[i];
            } else if (dist > maxDist[1]) {
                maxDist[1] = dist;
                workingOutlets[1] = outlets[i];
            } else {
                notWorkingOutlets.Add(outlets[i]);
            }
        }
        brokenOutlets = notWorkingOutlets.ToArray();
    }

    public Outlet[] ResetWorkingOutlets(Vector3 playerPosition) {
        FlipBreakers(playerPosition);
        return workingOutlets;
    }

    public Outlet[] GetWorkingOutlets() {
        return workingOutlets;
    }
}
