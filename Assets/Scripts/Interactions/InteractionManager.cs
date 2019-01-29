using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public TargetController controller;
    public List<Interaction> interactions;
    private bool isInit = false;

    void Initialize() {
        foreach (Interaction i in interactions)
        {
            i.Init(controller);
        }
        isInit = true;
    }

    public void UpdateInteractions()
    {
        if(controller != null && !isInit) {
            Initialize();
        }

        foreach (Interaction i in interactions)
        {
            i.UpdateInteraction(Time.deltaTime);
        }
    }
}
