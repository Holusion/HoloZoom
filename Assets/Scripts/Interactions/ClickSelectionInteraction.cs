using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelectionInteraction : Interaction
{
    public override void UpdateInteraction(float deltaTime)
    {
        // if(Input.GetButtonDown("Fire1"))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1))
        //     {
        //         this.controller.stateMachine.Step("select", () => this.controller.SetTarget(hit.collider.gameObject));
        //     }
        // }
        // else if (Input.GetButtonDown("Fire2"))
        // {
        //     this.controller.stateMachine.Step("unselect", () => this.controller.SetTarget(controller.initPos, true));
        // }
        // else if(Input.GetButton("Fire1") && this.controller.readyToRotate)
        // {
        //     this.controller.stateMachine.Step("rotate", () => this.controller.RotateTarget(Input.mousePosition));
        // }
    }
}
