using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour, ITargetAnswer
{
    public GameObject actionPanel;
    GameObject initPanel;

    void Start() {
        Transform background = actionPanel.transform.Find("Panel").Find("Background");
        initPanel = GameObject.Find("ActionPanelInitPos");
    }

    public void OnSelected(GameObject previousTarget) {
        if(previousTarget == GameObject.Find("InitPos")) {
            initPanel.SetActive(false);
        }
        actionPanel.SetActive(true);
    }

    public void OnUnselected(GameObject previousTarget) {
        if(previousTarget == GameObject.Find("InitPos")) {
            initPanel.SetActive(true);
        }
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
