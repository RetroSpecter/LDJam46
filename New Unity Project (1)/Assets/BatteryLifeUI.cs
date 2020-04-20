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
    public float redThreshold = 0.3f;
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

        AudioManager mgr = AudioManager.instance;

        if (ratio < redThreshold && ratio > 0)
        {
            if (ratio < flashingThreshold)
            {
                mgr.Stop("BeepSlow");
                if (!mgr.IsPlaying("BeepFast"))
                {
                    mgr.Play("BeepFast");
                }
            } else
            {
                mgr.Stop("BeepFast");
                if (!mgr.IsPlaying("BeepSlow"))
                {
                    mgr.Play("BeepSlow");
                }
            }
        } else
        {
            mgr.Stop("BeepSlow");
            mgr.Stop("BeepFast");
        }

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
