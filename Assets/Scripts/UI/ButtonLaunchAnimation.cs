using UnityEngine;

public class ButtonLaunchAnimation : MonoBehaviour, ButtonAction
{
    public GameObject objectToAnimate;
    public string triggerOn, triggerOff;
    public bool shouldStack = true;

    public void MakeAction(Player player)
    {
        player.CmdAnimate(objectToAnimate, triggerOn, triggerOff, shouldStack);
    }
}
