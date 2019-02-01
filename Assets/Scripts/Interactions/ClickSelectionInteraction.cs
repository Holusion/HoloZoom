using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelectionInteraction : Interaction
{
    private float lastSpeed;

    public override void UpdateInteraction(Player player)
    {
        float speed = Input.GetAxisRaw("Mouse X");

        if(Input.GetButtonDown(Player.BUTTON_LEFT)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1) && hit.transform.tag == "Selectable")
            {
                player.CmdTarget(Player.SELECT, hit.collider.gameObject);
            } else {
                player.CmdTarget(Player.UNSELECT, null);
            }
        } 
        else if(Input.GetButtonDown(Player.BUTTON_RIGHT)) 
        {
            player.CmdTarget(Player.UNSELECT, null);
        } 
        else if(Input.GetButton(Player.BUTTON_LEFT) && speed != 0)
        {
            player.CmdRotate(speed);
            lastSpeed = speed;
        } else {
            if(lastSpeed >= 0.001f || lastSpeed <= -0.001f) {
                lastSpeed /= 1.1f;
                player.CmdRotate(lastSpeed);
            }
        }
    }

    public override bool CanInteract(Player player) {
        return !player.IsUI();
    }
}
