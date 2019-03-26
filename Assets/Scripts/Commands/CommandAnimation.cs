using System.Text;
using UnityEngine;

public class CommandAnimation : Command
{
    GameObject go;
    string triggerOn, triggerOff;
    bool shouldStack;

    public CommandAnimation(GameObject go, string triggerOn, string triggerOff, bool shouldStack)
    {
        this.go = go;
        this.triggerOn = triggerOn;
        this.triggerOff = triggerOff;
        this.shouldStack = shouldStack;
    }

    public void Do()
    {
        Animate(triggerOn);
        if(this.shouldStack) {
            ActionPanelController previousActionPanel = go.GetComponent<ActionPanelController>();
            if(previousActionPanel != null) {
                previousActionPanel.actionPanel.SetActive(false);
            }
            TreeController tree = go.GetComponent<TreeController>();
            if(tree != null) {
                tree.Push(triggerOn);
            }
        }
    }

    public byte[] Serialize()
    {
        string packet = "Animation:" + go.name + ":" + triggerOn + ":" + triggerOff + ":" + (shouldStack ? 1 : 0);
        return Encoding.ASCII.GetBytes(packet);
    }

    public void Undo()
    {
        Animate(triggerOff);
        if(this.shouldStack) {
            ActionPanelController previousActionPanel = go.GetComponent<ActionPanelController>();
            if(previousActionPanel != null) {
                previousActionPanel.actionPanel.SetActive(true);
            }
            TreeController tree = go.GetComponent<TreeController>();
            if(tree != null) {
                tree.Pop(triggerOn);
            }
        }
    }

    private void Animate(string trigger) {
        int iTrigger = Animator.StringToHash(trigger);
        Animator anim = go.GetComponent<Animator>();
        anim.SetTrigger(iTrigger);
    }
}