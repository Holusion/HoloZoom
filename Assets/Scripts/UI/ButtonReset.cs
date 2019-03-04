using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour, ButtonAction
{
    public void MakeAction(Player player)
    {
        player.CmdTarget(Player.UNSELECT, null);
    }
}
