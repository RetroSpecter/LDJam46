using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class MenuButton : MonoBehaviour
{
    public UnityEvent OnPressed;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnHover() {
        anim.SetTrigger("Selected");
    }

    public void OnPress() {
        anim.SetTrigger("Pressed");
        OnPressed?.Invoke();
    }

    public void OnExitHover() {
        anim.SetTrigger("Deselected");
    }
}
