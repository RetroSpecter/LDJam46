using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RToReset : MonoBehaviour
{
    public int titleSceneIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(titleSceneIndex);
        }
    }
}
