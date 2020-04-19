using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryLifeUI : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image bar;
    [SerializeField] Image batteryFrame;
    [Space()]
    public Gradient barColorOverFill;
    [Range(0,1)]
    public float flashingThreshold = 0.2f;
    public float flashingRate = 10;
    // Update is called once per frame
    void Update()
    {
        float ratio = GameManager.instance.batteryTime / GameManager.instance.maxBatteryTime;
        UpdateUI(ratio);
    }

    void UpdateUI(float ratio) {
        ratio = Mathf.Clamp01(ratio);
        text.text = "" + Mathf.Round(ratio * 100) + "%";
        slider.value = ratio;

        Color curColor = barColorOverFill.Evaluate(ratio);
        bar.color = curColor;
        text.color = curColor;
        batteryFrame.color = curColor;
        flashing(Mathf.Sin(Time.time * flashingRate) < 0 || ratio > flashingThreshold);
    }

    void flashing(bool on) {
        bar.enabled = on;
        batteryFrame.enabled = on;
        slider.enabled = on;
        text.enabled = on;
    }
}
