using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetAnswer
{
    void OnSelected(GameObject previousTarget);
    void OnUnselected(GameObject previouslastTarget);
    void OnActive(bool enable);
    void OnDesactive();
}
