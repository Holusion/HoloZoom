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

    void FadeIn(Activator.ActivatorObject ao, int trigger) {
        ao.gameObject.SetActive(ao.initActive);

        Activator activator = ao.gameObject.GetComponent<Activator>();
        if(activator != null) {
            foreach(Activator.ActivatorObject child in activator.nextSelectable) {
                FadeIn(child, trigger);
            }
        }
    }

    void FadeOut(Activator.ActivatorObject ao, int trigger) {
        Animator anim = ao.gameObject.GetComponent<Animator>();
        if(anim != null) {
            anim.SetTrigger(trigger);
        }

        Activator activator = ao.gameObject.GetComponent<Activator>();
        if(activator != null) {
            foreach(Activator.ActivatorObject child in activator.nextSelectable) {
                FadeOut(child, trigger);
            }
        }
    }

    public void OnSelected(GameObject previousTarget)
    {
        int trigger = Animator.StringToHash("Close");
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(Activator.ActivatorObject ao in activator.nextSelectable) {
            if(ao.gameObject != this.gameObject) {
                FadeOut(ao, trigger);
            }
        }

        int index = this.gameObject.GetComponent<Activator>().nextSelectable.FindIndex(ao => ao.gameObject == previousTarget); 
        if(index < 0) {
            Animator previousTargetAnim = previousTarget.GetComponent<Animator>();
            if(previousTargetAnim != null) {
                previousTargetAnim.SetTrigger(trigger);
            }
        }
    }

    public void OnUnselected(GameObject previousTarget)
    {
        int trigger = Animator.StringToHash("Open");
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(Activator.ActivatorObject ao in activator.nextSelectable) {
            if(ao.gameObject != this.gameObject) {
                FadeIn(ao, trigger);
            }
        }

        previousTarget.SetActive(true);
    }

    public void OnDesactive() {
        this.gameObject.SetActive(false);
    }
}
