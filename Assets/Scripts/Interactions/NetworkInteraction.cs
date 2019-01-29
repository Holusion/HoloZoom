using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInteraction : Interaction
{
    private string lastClick = "";

    public override void UpdateInteraction(float deltaTime)
    {
        // if (controller.network.target != "" && lastClick != controller.network.target)
        // {
        //     if(controller.network.target == "reset")
        //     {
        //         this.controller.stateMachine.Step("unselect", () => controller.SetTarget(controller.initPos, true));
        //     } else
        //     {
        //         this.controller.stateMachine.Step("select", () => controller.SetTarget(GameObject.Find(controller.network.target)));
        //     }
        //     lastClick = controller.network.target;
        // } else if(lastClick == "reset")
        // {
        //     this.controller.stateMachine.Step("unselect", () => controller.SetTarget(controller.initPos, true));
        // }
    }

    public void Reset()
    {
        // if(lastClick != "reset")
        // {
        //     controller.SetTarget(GameObject.Find(lastClick));
        // }
    }

    public override void Init(TargetController controller)
    {
        base.Init(controller);
        controller.SetTarget(controller.initPos, true);
        lastClick = "";
    }
}
