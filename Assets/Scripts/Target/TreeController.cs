using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeController : MonoBehaviour, ITargetAnswer
{
    public Text treeUI;

    public void OnActive(bool enable)
    {

    }

    public void OnDesactive()
    {

    }

    public void OnRotate()
    {

    }

    public void OnSelected(GameObject previousTarget)
    {
        if(previousTarget == GameObject.Find("InitPos")) {
            treeUI.text = "";
        }
        int index = this.gameObject.GetComponent<Activator>().nextSelectable.FindIndex(ao => ao.gameObject == previousTarget); 
        if(index < 0) {
            treeUI.text += "<b>/</b>" + this.name;
        }
    }

    public void OnUnselected(GameObject previouslastTarget)
    {
        if(previouslastTarget == GameObject.Find("InitPos")) {
            treeUI.text = "Holozoom by <b>Holusion</b>";
        } else {
            treeUI.text = treeUI.text.Replace("<b>/</b>" + this.name, "");
        }
    }
}
