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

    public Stage currentStage {
        get;
        private set;
    }
    public event Action<Stage> OnStageChange;

    public Vector3 GetPlayerPosition() {
        return player.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Stage
{
    Stage1 = 0, Stage2 = 1, Stage3 = 2
}
