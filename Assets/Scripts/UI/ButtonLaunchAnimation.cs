using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLaunchAnimation : MonoBehaviour, ButtonAction
{
    public GameObject objectToAnimate;
    public string trigger;

    public void MakeAction(Player player)
    {
        int iTrigger = Animator.StringToHash(trigger);
        Animator anim = objectToAnimate.GetComponent<Animator>();
        anim.SetTrigger(iTrigger);
    }
}
