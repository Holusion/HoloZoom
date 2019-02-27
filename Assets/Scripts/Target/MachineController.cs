using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : TargetAnimationController, ITargetAnswer
{
    public new void OnUnselected(GameObject lastTarget) {
        base.OnUnselected(lastTarget);

        this.gameObject.SetActive(false);
    }
}
