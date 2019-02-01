using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour, ButtonAction
{
    public GameObject myRoom;
    private bool visible = true;

    public void MakeAction(Player player) {
        visible = !visible;
        player.CmdEnable(myRoom, visible);
    }
}
