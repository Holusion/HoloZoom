using System.Text;
using UnityEngine;

public class CommandSelection : Command
{
    TargetController controller;
    GameObject go, lastTarget;

    public CommandSelection(TargetController controller, GameObject go, GameObject lastTarget)
    {
        this.controller = controller;
        this.go = go;
        this.lastTarget = lastTarget;
    }

    public void Do()
    {
        int index = controller.target.GetComponent<Activator>().nextSelectable.FindIndex(ao => ao.gameObject == go);

        if(index >= 0) {
            controller.targetChange = true;
            lastTarget = controller.target;
            
            controller.target = go;
            ITargetAnswer[] answers = controller.target.GetComponents<ITargetAnswer>();
            foreach(ITargetAnswer answer in answers) {
                if(answer != null) {
                    answer.OnSelected(lastTarget);
                }
            }
        }
    }

    public byte[] Serialize()
    {
        string packet = "SELECTION:" + go.name + ":" + lastTarget.name;
        return Encoding.ASCII.GetBytes(packet);
    }

    public void Undo()
    {
        GameObject previousTarget = lastTarget;
        ITargetAnswer[] answers = controller.target.GetComponents<ITargetAnswer>();
        foreach(ITargetAnswer answer in answers) {
            if(answer != null) {
                answer.OnUnselected(previousTarget);
            }
        }
        controller.targetChange = true;
        GameObject tmp = controller.target;
        controller.target = previousTarget;
    }
}