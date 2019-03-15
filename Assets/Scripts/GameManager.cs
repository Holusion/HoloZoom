﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public bool isClient;
    public GameObject client;
    public GameObject[] interactiveObjectWithBubbleText;
    public NetworkHolusion networkHolusion;

    [Header("Camera Settings")]
    public GameObject cam;
    public float minFarClip = 25;
    public float maxFarClip = 300;
    public float minFOV = 25;
    public float maxFOV = 60;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    void Update() {
        this.UpdateView();
    }

    void UpdateView() {
        this.client.SetActive(isClient);
        this.networkHolusion.isClient = !this.isClient;

        foreach(GameObject go in interactiveObjectWithBubbleText) {
            go.transform.Find("BubbleText").gameObject.SetActive(!this.isClient);
        }

        Camera.main.clearFlags = isClient ? CameraClearFlags.Skybox : CameraClearFlags.SolidColor;
    }
}
