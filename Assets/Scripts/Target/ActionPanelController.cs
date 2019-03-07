using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : TargetAnimationController, ITargetAnswer
{
    public GameObject actionPanel;

    void Start() {
        Transform background = actionPanel.transform.Find("Panel").Find("Background");
    }

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);
        actionPanel.SetActive(true);
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);
        actionPanel.SetActive(false);
    }

    public new void OnDesactive() {
        base.OnDesactive();
        actionPanel.SetActive(false);
    }
}
