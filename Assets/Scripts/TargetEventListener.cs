using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TargetEventListener
{
    void TargetChange(GameObject target);
    void TargetHasChanged(GameObject target);
    void Reset();
}
