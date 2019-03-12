using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour, ITargetAnswer
{
    public GameObject actionPanel;
    GameObject initPanel;

    void Start() {
        if(actionPanel != null) {
            Transform background = actionPanel.transform.Find("Panel").Find("Background");
            initPanel = GameObject.Find("ActionPanelInitPos");
        }
    }

    public void OnSelected(GameObject previousTarget) {
        ActionPanelController previousActionPanel = previousTarget.GetComponent<ActionPanelController>();
        if(previousActionPanel != null) {
            previousActionPanel.actionPanel.SetActive(false);
        }
        if(actionPanel != null) {
            actionPanel.SetActive(true);
        }
    }

    public void OnUnselected(GameObject previousTarget) {
        ActionPanelController previousActionPanel = previousTarget.GetComponent<ActionPanelController>();
        if(previousActionPanel != null) {
            previousActionPanel.actionPanel.SetActive(true);
        }
        if(actionPanel != null) {
            actionPanel.SetActive(false);
        } 
    }

    public void OnDesactive() {
        if(actionPanel != null) {
            actionPanel.SetActive(false);
        }
    }

    public void OnActive(bool enable)
    {
    }

    public void OnRotate()
    {
    }
}
