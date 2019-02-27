using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENSAITController : TargetAnimationController, ITargetAnswer
{
    public Button elecButton;
    public Button resetButton;
    public Button machineButton;

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);

        elecButton.gameObject.SetActive(true);
        machineButton.gameObject.SetActive(true);

        elecButton.onClick.AddListener(() => {
            resetButton.gameObject.SetActive(true);
            elecButton.gameObject.SetActive(false);
        });

        resetButton.onClick.AddListener(() => {
            elecButton.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(false);
        });
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);

        elecButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        machineButton.gameObject.SetActive(false);

        Animator anim = this.gameObject.GetComponent<Animator>();
        int trigger = Animator.StringToHash("Open");
        anim.SetTrigger(trigger);
    }

    public new void OnDesactive() {
        base.OnDesactive();

        elecButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        machineButton.gameObject.SetActive(false);
    }
}
