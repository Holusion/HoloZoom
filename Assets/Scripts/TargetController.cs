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
	}
	
	// Update is called once per frame
	void Update () {
        if(reset && this.lastTarget != null)
        {
            this.readyToRotate = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, initRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, initPos.transform.position, zoomSpeed * Time.deltaTime);

            Vector3 targetPosition = target.transform.position;
            ChangeAlpha(lastTarget.transform.position, targetPosition);
            if(Vector3.Distance(transform.position, targetPosition) < 0.001)
            {
                this.stateMachine.Step("", () => {});
                this.FireReset();
            }
        }

        if(targetChange && !reset)
        {
            Vector3 direction = target.GetComponent<BoxCollider>().bounds.center - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this.readyToRotate = false;

            Vector3 targetPosition = target.transform.Find("CamPos").transform.position;
            transform.position = Vector3.Lerp(transform.position,targetPosition, zoomSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1 && Quaternion.Angle(toRotation, transform.rotation) < 1)
            {
                targetChange = false;
                lastTarget = target.gameObject;
                this.FireTargetHasChanged();
                this.readyToRotate = true;
            }

            ChangeAlpha(initPos.transform.position, targetPosition);
        }

        // if(readyToRotate)
        // {
        //     this.transform.RotateAround(this.target.transform.position, Vector3.up, this.angularVelocity * rotationSpeed);
        //     this.angularVelocity /= 1.1f;
        // }
    }

    void ChangeAlpha(Vector3 initialPos, Vector3 targetPosition)
    {
        for(int i = 0; i < interactiveTarget.transform.childCount; i++)
        {
            Transform child = interactiveTarget.transform.GetChild(i);
            FadeChild(child, fadeRatio);
        }
    }

    void FadeValue(Material m, float value)
    {
        Color c = m.color;
        c.a += value;
        c.a = Mathf.Clamp(c.a, 0, 1);
        m.color = c;
    }

    void FadeChild(Transform child, float ratio)
    {
        MeshRenderer renderer = child.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material[] m = child.GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < m.Length; i++)
            {
                if (!target.transform.Equals(child) && !reset)
                {
                    this.FadeValue(m[i], -ratio);
                }
                else
                {
                    this.FadeValue(m[i], ratio);
                }
            }
        }
        if(child.childCount > 0)
        {
            for(int i = 0; i < child.childCount; i++)
            {
                FadeChild(child.GetChild(i), ratio);
            }
        }
    }

    public void SetTarget(GameObject go, bool reset = false)
    {
        this.target = go;
        this.reset = reset;
        if(!reset)
        {
            targetChange = true;
        }
        this.FireTargetChange();
    }

    public void RotateTarget(float speed)
    {
        this.target.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("SetEmpty");
        this.transform.RotateAround(this.target.transform.position, Vector3.up, speed * rotationSpeed);
        // this.angularVelocity = speed;
        // if(angularVelocity < -maxAngularSpeed) {
        //     angularVelocity = -maxAngularSpeed;
        // }
        // if(angularVelocity > maxAngularSpeed) {
        //     angularVelocity = maxAngularSpeed;
        // }
        // Debug.Log(angularVelocity);
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
