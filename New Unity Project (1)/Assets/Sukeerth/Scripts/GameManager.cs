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
    public Transform roomDoor;
    public Transform gate;

    public Stage currentStage {
        get;
        private set;
    }
    public event Action<Stage> OnStageChange;
    public event Action OnFinishedCharging;
    public event Action OnObstacleCollide;


    public void Colliding()
    {
        OnObstacleCollide?.Invoke();
    }

    public Vector3 GetPlayerPosition() {
        return player.position;
    }

    public void ChangeStage(int index) {
        switch (index) {
            case 1:
                StartCoroutine(RotateToOpen(roomDoor, new Vector3(-90, 0, -17)));
                break;
            case 2:
                StartCoroutine(RotateToOpen(gate, new Vector3(0, -153, 0)));
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartedCharging() {
        Invoke("FinishedCharging", chargeTime);
    }

    private void FinishedCharging() {
        OnFinishedCharging?.Invoke();
    }
}

public enum Stage
{
    Stage1 = 0, Stage2 = 1, Stage3 = 2
}
