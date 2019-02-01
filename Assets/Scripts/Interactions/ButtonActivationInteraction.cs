using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonActivationInteraction : Interaction
{
    public override void UpdateInteraction(Player player) {

        if(Input.GetButtonDown(Player.BUTTON_LEFT)) {
            EventSystem.current.currentSelectedGameObject.GetComponent<ButtonAction>().MakeAction(player);
        }
    }

    public override bool CanInteract(Player player) {
        return player.IsUI();
    }
}
