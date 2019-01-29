using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : ScriptableObject
{

    protected TargetController controller;
    
    public virtual void Init(TargetController controller)
    {
        this.controller = controller;
    }

    public abstract void UpdateInteraction(float deltaTime);
}
