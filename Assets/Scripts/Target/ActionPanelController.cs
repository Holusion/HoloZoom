using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour, ITargetAnswer
{
    public GameObject actionPanel;

    void Start() {
        Transform background = actionPanel.transform.Find("Panel").Find("Background");
    }

    public void OnSelected(GameObject previousTarget) {
        actionPanel.SetActive(true);
    }

    public void OnUnselected(GameObject previousTarget) {
        actionPanel.SetActive(false);
    }

    public void OnDesactive() {
        actionPanel.SetActive(false);
    }

    public void OnActive(bool enable)
    {
    }

    public void OnRotate()
    {
    }
}
