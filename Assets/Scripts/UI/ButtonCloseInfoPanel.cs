﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCloseInfoPanel : MonoBehaviour, ButtonAction
{
    public GameObject infoPanel;

    public void MakeAction(Player player) {
        infoPanel.SetActive(false);
    }
}