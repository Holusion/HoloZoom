using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimationController : MonoBehaviour, ITargetAnswer
{
    public void OnActive(bool enable)
    {
        if(enable) this.gameObject.SetActive(true);
        int trigger = enable ? Animator.StringToHash("Open") : Animator.StringToHash("Close");
        Animator anim = this.GetComponent<Animator>();
        anim.SetTrigger(trigger);
    }

    public void OnSelected(GameObject previousTarget)
    {
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(GameObject go in activator.nextSelectable) {
            if(go != this.gameObject) {
                Animator anim = go.GetComponent<Animator>();
                int trigger = Animator.StringToHash("Close");
                anim.SetTrigger(trigger);
            }
        }
    }

    public void OnUnselected(GameObject previousTarget)
    {
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(GameObject go in activator.nextSelectable) {
            if(go != this.gameObject) {
                go.SetActive(true);
                Animator anim = go.GetComponent<Animator>();
                int trigger = Animator.StringToHash("Open");
                anim.SetTrigger(trigger);
            }
        }
    }

    public void OnDesactive() {
        this.gameObject.SetActive(false);
    }
}
