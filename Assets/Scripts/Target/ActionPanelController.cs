using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour, ITargetAnswer
{
    public GameObject actionPanel;
    GameObject initPanel;
    public float animationTime = 0.75f;
    GameObject previousActionPanel;

    void Start() {
        if(actionPanel != null) {
            Transform background = actionPanel.transform.Find("Panel").Find("Background");
            initPanel = GameObject.Find("ActionPanelInitPos");
        }
    }

    IEnumerator LaunchAnim(GameObject previousActionPanel, GameObject actionPanel) {
        if(previousActionPanel != null) {
            previousActionPanel.GetComponent<Animator>().SetTrigger("Close");
            yield return new WaitForSeconds(animationTime);
            previousActionPanel.SetActive(false);
        }
        if(actionPanel != null) {
            actionPanel.SetActive(true);
        }
    }

    public void OnSelected(GameObject previousTarget) {
        GameObject previousActionPanel = null;
        if(previousTarget.GetComponent<ActionPanelController>() != null) {
            previousActionPanel = previousTarget.GetComponent<ActionPanelController>().actionPanel;
        }
        this.previousActionPanel = previousActionPanel;
        StartCoroutine(LaunchAnim(previousActionPanel, actionPanel));
    }

    public void OnUnselected(GameObject previousTarget) {
        GameObject previousActionPanel = null;
        if(previousTarget.GetComponent<ActionPanelController>() != null) {
            previousActionPanel = previousTarget.GetComponent<ActionPanelController>().actionPanel;
        }
        StartCoroutine(LaunchAnim(actionPanel, previousActionPanel));
    }

    public void OnDesactive() {
        //if(actionPanel == null && this.previousActionPanel != null) {
        //    this.previousActionPanel.SetActive(true);
        //}
        //if(actionPanel != null) {
        //    actionPanel.SetActive(false);
        //}
    }

    public void OnActive(bool enable)
    {
    }

    public void OnRotate()
    {
    }
}
