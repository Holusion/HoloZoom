using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public bool isClient;
    public GameObject canvas;
    public GameObject[] interactiveObjectWithBubbleText;
    public NetworkHolusion networkHolusion;

    private bool skyChange = false;

    // Start is called before the first frame update
    void Start()
    {    
        // this.UpdateView();
    }
#if UNITY_EDITOR
    void Update() {
        // if(Camera.main != null && !this.skyChange) {
        //     this.skyChange = true;
        // }
        this.UpdateView();
    }
#endif
    void UpdateView() {
        this.canvas.SetActive(isClient);
        this.networkHolusion.isClient = !this.isClient;

        foreach(GameObject go in interactiveObjectWithBubbleText) {
            go.transform.Find("BubbleText").gameObject.SetActive(!this.isClient);
        }
        Camera.main.clearFlags = isClient ? CameraClearFlags.Skybox : CameraClearFlags.SolidColor;
    }
}
