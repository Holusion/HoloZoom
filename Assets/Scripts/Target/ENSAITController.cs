using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENSAITController : TargetAnimationController, ITargetAnswer
{
    public Button elecButton;

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);

        elecButton.gameObject.SetActive(true);
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);

        elecButton.gameObject.SetActive(false);
    }
}
