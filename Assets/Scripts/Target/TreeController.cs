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
        Debug.Log(previousTarget);
        int index = this.gameObject.GetComponent<Activator>().nextSelectable.FindIndex(ao => ao.gameObject == previousTarget); 
        if(index < 0) {
            treeUI.text += this.name + "<b>/</b>";
        }
    }

    public void OnUnselected(GameObject previouslastTarget)
    {
        treeUI.text = treeUI.text.Replace(this.name + "<b>/</b>", "");
    }
}
