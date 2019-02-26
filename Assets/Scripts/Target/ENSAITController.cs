using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENSAITController : TargetAnimationController, ITargetAnswer
{
    public Button elecButton;
    public Button reset;

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);

        elecButton.gameObject.SetActive(true);
        elecButton.onClick.AddListener(() => {
            reset.gameObject.SetActive(true);
            elecButton.gameObject.SetActive(false);
        });

        reset.onClick.AddListener(() => {
            elecButton.gameObject.SetActive(true);
            reset.gameObject.SetActive(false);
        });
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);

        elecButton.gameObject.SetActive(false);
        Animator anim = this.gameObject.GetComponent<Animator>();
        int trigger = Animator.StringToHash("Open");
        anim.SetTrigger(trigger);
    }
}
