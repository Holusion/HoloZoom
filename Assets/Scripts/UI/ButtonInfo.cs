﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour, ButtonAction
{
    public GameObject infoPanel;

    public void MakeAction(Player player) {
        infoPanel.SetActive(true);
    }
}
