using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName="Button activation", menuName="Interactions/Button activation")]
public class ButtonActivationInteraction : Interaction
{
    public override void UpdateInteraction(Player player) {

        if(Input.GetButtonDown(Player.BUTTON_LEFT)) {
            EventSystem.current.currentSelectedGameObject.GetComponent<ButtonAction>().MakeAction(player);
        }
    }

    public override bool CanInteract(Player player) {
        // return player.IsUI();
        return false;
    }
}
