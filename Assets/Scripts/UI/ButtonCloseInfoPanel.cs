using UnityEngine;

public class ButtonCloseInfoPanel : MonoBehaviour, ButtonAction
{
    public GameObject infoPanel;

    public void MakeAction(Player player) {
        infoPanel.SetActive(false);
    }
}
