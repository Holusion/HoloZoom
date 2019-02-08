using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetController : NetworkBehaviour {

    public GameObject interactiveTarget;
    public GameObject target;
    public float fadeRatio = 0.1f;
    public float rotationSpeed = 0.1f;
    public float zoomSpeed = 5f;
    public float maxAngularSpeed = 5f;
    bool targetChange = false;
    bool reset = true;
    Quaternion initRotation;
    GameObject lastTarget;
    public GameObject initPos;

    private List<TargetEventListener> listeners;
    public ZoomStateMachine stateMachine { get; private set; }

    private Vector2 previousMousePosition;
    public bool readyToRotate = false;
    private float angularVelocity = 0;
    private Dictionary<GameObject, bool> fadeAcc = new Dictionary<GameObject, bool>();

    void Awake()
    {
        this.listeners = new List<TargetEventListener>();
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
    }

    // Use this for initialization
    void Start () {
        this.initRotation = transform.rotation;
        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
        this.target = initPos;
        this.lastTarget = null;
        this.stateMachine = new ZoomStateMachine();
        this.target = this.initPos;
	}
	
	// Update is called once per frame
	void Update () {
        if(targetChange)
        {
            Vector3 direction;
            Vector3 targetPosition;
            Quaternion toRotation;

            if(target.GetComponent<BoxCollider>() != null) {
                direction = target.GetComponent<BoxCollider>().bounds.center - transform.position;
                targetPosition = target.transform.Find("CamPos").transform.position;
                toRotation = Quaternion.LookRotation(direction);
            } else {
                targetPosition = target.transform.position;
                toRotation = initRotation;
            }

            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this.readyToRotate = false;

            transform.position = Vector3.Lerp(transform.position,targetPosition, zoomSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1 && Quaternion.Angle(toRotation, transform.rotation) < 1)
            {
                targetChange = false;
                this.FireTargetHasChanged();
                this.readyToRotate = true;
            }
        }

        ComputeFade();
    }

    private void ComputeFade() {
        foreach(KeyValuePair<GameObject, bool> item in fadeAcc) {
            Fade(item.Key, item.Value);
        }
        
        List<GameObject> gameObjects = new List<GameObject>(fadeAcc.Keys);
        foreach(GameObject item in gameObjects) {
            float fadeValue = GetFadeValue(item);

            if(fadeValue <= 0 || fadeValue >= 1) {
                fadeAcc.Remove(item);
            }
            if(fadeValue <= 0) item.transform.parent.gameObject.SetActive(false);
            else item.transform.parent.gameObject.SetActive(true);
        }

    }

    public void FadeAllExcept(GameObject selected, bool fadeIn)
    {
        FadeChild(this.interactiveTarget, selected, fadeIn);
    }

    public void FadeExceptTargetActivator(GameObject activator, bool fadeIn) 
    {
        FadeChildActivator(this.interactiveTarget, activator, fadeIn);
    }

    public void FadeInActivator(GameObject activator) {
        foreach(GameObject go in activator.GetComponent<Activator>().nextSelectable) {
            FadeChild(this.interactiveTarget, go, true);
        }
        FadeChild(this.interactiveTarget, activator, true);
    }

    public void FadeOne(GameObject selected, bool fadeIn)
    {
        fadeAcc.Remove(selected);
        fadeAcc.Add(selected, fadeIn);
        FadeChild(selected, null, fadeIn);
    }

    private void FadeChild(GameObject start, GameObject selected, bool fadeIn) {
        foreach(Transform child in start.transform)
        {
            if(child.gameObject != selected) {
                fadeAcc.Remove(child.gameObject);
                fadeAcc.Add(child.gameObject, fadeIn);
                FadeChild(child.gameObject, selected, fadeIn);
            }
        }
    }

    private void FadeChildActivator(GameObject start, GameObject activator, bool fadeIn) {
        foreach(Transform child in start.transform)
        {
            if(!activator.GetComponent<Activator>().nextSelectable.Contains(child.gameObject) && child.gameObject != activator) {
                fadeAcc.Remove(child.gameObject);
                fadeAcc.Add(child.gameObject, fadeIn);
                FadeChildActivator(child.gameObject, activator, fadeIn);
            }
        }
    }

    void FadeValue(Material m, float value)
    {
        Color c = m.color;
        c.a += value;
        c.a = Mathf.Clamp(c.a, 0, 1);
        m.color = c;
    }

    float GetFadeValue(GameObject go) {
        MeshRenderer renderer = go.transform.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material[] m = renderer.materials;
            for(int i = 0; i < m.Length; i++)
            {
                return m[i].color.a;
            }
        }
        return 2;
    }

    public void Fade(GameObject go, bool fadeIn)
    {
        MeshRenderer renderer = go.transform.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material[] m = renderer.materials;
            for(int i = 0; i < m.Length; i++)
            {
                this.FadeValue(m[i], fadeRatio * (fadeIn ? 1 : -1));
            }
        }
    }

    public void SetTarget(GameObject go, bool reset = false)
    {
        if(this.lastTarget == null || target.GetComponent<Activator>().nextSelectable.Contains(go) && !reset) {
            targetChange = true;
            FadeExceptTargetActivator(go, false);
            lastTarget = this.target;
            this.target = go;
        } else {
            targetChange = true;
            FadeInActivator(lastTarget);
            GameObject tmp = this.target;
            this.target = lastTarget;
            lastTarget = tmp;
        }
        
        this.reset = reset;

        this.FireTargetChange();
    }

    public void RotateTarget(float speed)
    {
        if(this.target.GetComponent<BoxCollider>() != null) {
            this.transform.RotateAround(this.target.transform.TransformPoint(this.target.GetComponent<BoxCollider>().center), Vector3.up, speed * rotationSpeed);
        }
    }

    void FireTargetChange()
    {
        foreach (TargetEventListener l in listeners)
        {
            l.TargetChange(target);
        }
    }

    void FireTargetHasChanged()
    {
        foreach (TargetEventListener l in listeners)
        {
            l.TargetHasChanged(target);
        }
    }

    void FireReset()
    {
        foreach (TargetEventListener l in listeners)
        {
            l.Reset();
        }
    }
}
