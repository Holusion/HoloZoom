using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTextAnimationController : MonoBehaviour, ITargetAnswer
{
    public void OnSelected(GameObject previousTarget)
    {
        Activator activator = previousTarget.GetComponent<Activator>();
        foreach(GameObject go in activator.nextSelectable) {
            if(go != this.gameObject) {
                GameObject bubbleText = go.transform.Find("BubbleText").gameObject;
                Animator anim = go.transform.Find("BubbleText").gameObject.GetComponent<Animator>();
                int trigger = Animator.StringToHash("Close");
                anim.SetTrigger(trigger);
            }
        }
    }

    public void OnUnselected(GameObject previousTarget)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
