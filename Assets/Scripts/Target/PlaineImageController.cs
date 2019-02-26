using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaineImageController : TargetAnimationController, ITargetAnswer
{
    public Button _2020;
    public Button _2021;

    public new void OnSelected(GameObject previousTarget) {
        base.OnSelected(previousTarget);

        _2020.gameObject.SetActive(true);
        _2021.gameObject.SetActive(true);
    }

    public new void OnUnselected(GameObject previousTarget) {
        base.OnUnselected(previousTarget);

        _2020.gameObject.SetActive(false);
        _2021.gameObject.SetActive(false);
    }
}
