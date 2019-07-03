using UnityEngine;

public class ButtonSelection : MonoBehaviour, ButtonAction
{
    public GameObject selectedGameObject;

    public void MakeAction(Player player)
    {
        player.CmdTargets(new string[] {selectedGameObject.name});
    }
}
