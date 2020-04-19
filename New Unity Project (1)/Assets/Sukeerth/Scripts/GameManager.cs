using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    private Transform player;
    public float chargeTime = 2f;
    public float downTime = 2f;
    public Transform roomDoor;
    public Transform gate;

    public float batteryTime;
    [HideInInspector]public float maxBatteryTime;
    public float chargeAmount = 10;

    public Stage currentStage {
        get;
        private set;
    }
    public bool isInvulnerable;
    public event Action<Stage> OnStageChange;
    public event Action<Outlet> OnFinishedCharging;
    public event Action<GameObject> PlayerFall;
    public event Action PlayerStand;
    public event Action GameOver;

    public Vector3 GetPlayerPosition() {
        return player.position;
    }

    public void ChangeStage(int index) {
        switch (index) {
            case 1:
                StartCoroutine(RotateToOpen(roomDoor, new Vector3(-90, 0, -17)));
                currentStage = Stage.Stage2;
                break;
            case 2:
                StartCoroutine(RotateToOpen(gate, new Vector3(0, -153, 0)));
                currentStage = Stage.Stage3;
                break;
            default:
                break;
        }
    }

    private IEnumerator RotateToOpen(Transform rotatingObject, Vector3 goalRot) {
        float startTime = Time.time;
        Quaternion rot = Quaternion.Euler(goalRot);
        while (Time.time  - startTime < 2) {
            rotatingObject.rotation = Quaternion.Slerp(rotatingObject.rotation,
                rot, (Time.time - startTime) / chargeTime);
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        OutletManager.instance.ChargingAtOutlet += StartedCharging;
        maxBatteryTime = batteryTime;
    }

    private void Update() {
        batteryTime = Mathf.Clamp(batteryTime - Time.deltaTime, 0, maxBatteryTime);

        if(batteryTime <= 0 && GameOver !=null) {
            GameOver.Invoke();
            GameOver = null;
        }
    }

    private void StartedCharging(Outlet outlet) {
        StartCoroutine(FinishedCharging(outlet));
    }

    private IEnumerator FinishedCharging(Outlet outlet) {
        float t = 0;
        float startBatteryTime = batteryTime;
        while (t < chargeTime) {
            batteryTime = Mathf.Lerp(startBatteryTime, startBatteryTime + chargeAmount, t/chargeTime);
            t += Time.deltaTime;
            yield return null;
        }
        batteryTime = startBatteryTime + chargeAmount;
        OnFinishedCharging?.Invoke(outlet);
    }

    public void Colliding(GameObject o)
    {

        PlayerFall?.Invoke(o);
        StartCoroutine(StandingUp());
    }

    private IEnumerator StandingUp()
    {
        yield return new WaitForSeconds(downTime);
        PlayerStand?.Invoke();
    }
}

public enum Stage
{
    Stage1 = 0, Stage2 = 1, Stage3 = 2
}
