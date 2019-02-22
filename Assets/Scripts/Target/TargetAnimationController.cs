using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimationController : MonoBehaviour, ITargetAnswer
{
    public void OnRotate() {}
    public void OnActive(bool enable)
    {
        if(enable) this.gameObject.SetActive(true);
        int trigger = enable ? Animator.StringToHash("Open") : Animator.StringToHash("Close");
        Animator anim = this.GetComponent<Animator>();
        anim.SetTrigger(trigger);
    }

    public void OnSelected(GameObject previousTarget)
    {
        int trigger = Animator.StringToHash("Close");
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(GameObject go in activator.nextSelectable) {
            if(go != this.gameObject) {
                Animator anim = go.GetComponent<Animator>();
                if(anim != null) {
                    anim.SetTrigger(trigger);
                }
            }
        }

        Animator previousTargetAnim = previousTarget.GetComponent<Animator>();
        if(previousTargetAnim != null) {
            previousTargetAnim.SetTrigger(trigger);
        }
    }

    public void OnUnselected(GameObject previousTarget)
    {
        int trigger = Animator.StringToHash("Open");
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(GameObject go in activator.nextSelectable) {
            if(go != this.gameObject) {
                go.SetActive(true);
                Animator anim = go.GetComponent<Animator>();
                anim.SetTrigger(trigger);
            }
        }

        Animator previousTargetAnim = previousTarget.GetComponent<Animator>();
        previousTarget.SetActive(true);
        if(previousTargetAnim != null) {
            previousTargetAnim.SetTrigger(trigger);
        }
    }

    public void OnDesactive() {
        this.gameObject.SetActive(false);
    }
}
