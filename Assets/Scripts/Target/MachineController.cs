using UnityEngine;

public class MachineController : TargetAnimationController, ITargetAnswer
{
    public new void OnSelected(GameObject lastTarget) {
        base.OnSelected(lastTarget);
        this.gameObject.SetActive(true);
    }

    public new void OnUnselected(GameObject lastTarget) {
        base.OnUnselected(lastTarget);
        this.GetComponent<Animator>().SetTrigger("Close");
    }
}
