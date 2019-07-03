using UnityEngine;

public class ButtonActivation : MonoBehaviour, ButtonAction
{
    public GameObject myRoom;

    public void MakeAction(Player player) {
        // player.CmdEnable(myRoom, !myRoom.activeSelf);
    }
}
