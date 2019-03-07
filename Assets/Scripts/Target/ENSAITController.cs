using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENSAITController : TargetAnimationController, ITargetAnswer
{
    private Button elecButton;
    private Button machineButton;

    public GameObject actionPanel;

    void Start() {
        Transform background = actionPanel.transform.Find("Panel").Find("Background");
        elecButton = background.Find("ENSAIT_Elec").GetComponent<Button>();
        machineButton = background.Find("ENSAIT_Machine").GetComponent<Button>();
    }

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);
        actionPanel.SetActive(true);
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);
        actionPanel.SetActive(false);

        Animator anim = this.gameObject.GetComponent<Animator>();
        int trigger = Animator.StringToHash("Open");
        anim.SetTrigger(trigger);
    }

    public new void OnDesactive() {
        base.OnDesactive();
        actionPanel.SetActive(false);
    }
}
