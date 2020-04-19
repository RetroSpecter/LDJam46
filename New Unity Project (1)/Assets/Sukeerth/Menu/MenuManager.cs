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
        float input = Input.GetAxis("Vertical");
        if (input == 0) {
            return;
        }
        lastSelectTime = Time.time;
        buttons[index].OnExitHover();
        index += (int)Mathf.Sign(input);
        index = Mathf.Clamp(index, 0, buttons.Length - 1);
        buttons[index].OnHover();
    }
}
