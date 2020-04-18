using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetOutlets(Stage currentStage) {
        return 5;
    }
}
