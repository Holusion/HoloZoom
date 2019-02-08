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

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, 1);

            if(hits.Length == 0) {
                player.CmdTarget(Player.UNSELECT, null);
            } else {
                string[] gameObjectsNames = new string[hits.Length];
                for(int i = 0; i < gameObjectsNames.Length; i++) {
                    gameObjectsNames[i] = hits[i].collider.gameObject.name;
                }
                player.CmdTargets(Player.SELECT, gameObjectsNames);
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
