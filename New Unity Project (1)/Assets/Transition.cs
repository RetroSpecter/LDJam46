using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{

    public string gameOver = "End Game";
    public Image transitionMask;

    // Start is called before the first frame update
    void Start() {
        transitionIn();
        GameManager.instance.GameOver += transitionOut;
    }

    void transitionIn() {
        updateVignette(1);
        StartCoroutine(transitionInEnum(1));
    }

    void transitionOut() {
        StartCoroutine(transitionOutEnum(2));
    }

    IEnumerator transitionInEnum(float time) {
        float t = 0;
        while (t < time)
        {
            updateVignette(Mathf.Lerp(1, 0, t / time));
            t += Time.deltaTime;
            yield return null;
        }
        updateVignette(0);
    }

    IEnumerator transitionOutEnum(float time) {
        yield return new WaitForSeconds(3);
        float t = 0; 
        while (t < time) {
            updateVignette(Mathf.Lerp(0, 1, t/time));
            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameOver);
    }

    void updateVignette(float amount) {
        transitionMask.fillAmount = amount;
    }
}
