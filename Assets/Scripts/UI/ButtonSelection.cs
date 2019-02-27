using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelection : MonoBehaviour, ButtonAction
{
    public GameObject selectedGameObject;

    public void MakeAction(Player player)
    {
        player.CmdTargets(Player.SELECT, new string[] {selectedGameObject.name});
    }
}
