using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public float inputDelay = 0.2f;
    public MenuButton[] buttons;

    private int index;
    private float lastSelectTime;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0]?.OnHover();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSelectTime < inputDelay) {
            return;
        }
        if (Input.GetButtonDown("Submit")) {
            buttons[index].OnPress();
            return;
        }
        float input = Input.GetAxis("Horizontal");
        if (input == 0) {
            return;
        }
        lastSelectTime = Time.time;
        buttons[index].OnExitHover();
        index += (int)-Mathf.Sign(input);
        if (index >= buttons.Length) {
            index = 0;
        } else if (index < 0) {
            index = buttons.Length - 1;
        }
        buttons[index].OnHover();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void LoadLevel(int buildIndex) {
        StartCoroutine(Load(buildIndex));
    }

    private IEnumerator Load(int buildIndex) {
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }
}
