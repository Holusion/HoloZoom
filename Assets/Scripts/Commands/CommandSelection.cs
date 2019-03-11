using UnityEngine;

public class CommandSelection : IDoUndo
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
        if(lastTarget != null) {
            ITargetAnswer[] newAnswers = controller.target.GetComponents<ITargetAnswer>();
            foreach(ITargetAnswer answer in newAnswers) {
                answer.OnSelected(tmp);
            }
        }   
    }
}