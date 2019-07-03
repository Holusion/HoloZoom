﻿using UnityEngine;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public bool isClient;
    public GameObject canvas;
    public GameObject[] interactiveObjectWithBubbleText;

    private bool skyChange = false;

#if UNITY_EDITOR
    void Update() {
        this.UpdateView();
    }
#endif
    void UpdateView() {
        this.canvas.SetActive(isClient);

        foreach(GameObject go in interactiveObjectWithBubbleText) {
            go.transform.Find("BubbleText").gameObject.SetActive(!this.isClient);
        }
        Camera.main.clearFlags = isClient ? CameraClearFlags.Skybox : CameraClearFlags.SolidColor;
    }
}
