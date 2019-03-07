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
        treeUI.text += this.name + "<b>/</b>";
    }

    public void OnUnselected(GameObject previouslastTarget)
    {
        treeUI.text = treeUI.text.Replace(this.name + "<b>/</b>", "");
    }
}
