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
    public float maxAngularSpeed = 10f;
    bool targetChange = false;
    bool reset = true;
    Quaternion initRotation;
    Stack<GameObject> lastTarget;
    public GameObject initPos;
    public ZoomStateMachine stateMachine { get; private set; }

    public bool readyToRotate = false;
    private Dictionary<GameObject, bool> fadeAcc = new Dictionary<GameObject, bool>();

    void Awake()
    {
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
        this.lastTarget = new Stack<GameObject>();
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
                this.readyToRotate = true;
            }
        }
    }

    public void SetTarget(GameObject go, bool reset = false)
    {
        if(this.lastTarget.Count == 0 && go != null || target.GetComponent<Activator>().nextSelectable.Contains(go) && !reset) {
            targetChange = true;
            lastTarget.Push(this.target);
            this.target = go;
            ITargetAnswer answer = this.target.GetComponent<ITargetAnswer>();
            answer.OnSelected(lastTarget.Peek());
        } else if(reset && this.lastTarget.Count > 0) {
            GameObject previousTarget = lastTarget.Pop();
            ITargetAnswer answer = this.target.GetComponent<ITargetAnswer>();
            answer.OnUnselected(previousTarget);
            targetChange = true;
            GameObject tmp = this.target;
            this.target = previousTarget;
        }
        
        this.reset = reset;
    }

    public void RotateTarget(float speed)
    {
        float tmpSpeed = speed;
        if(this.target.GetComponent<BoxCollider>() != null && readyToRotate) {
            if(speed >= maxAngularSpeed) {
                tmpSpeed = maxAngularSpeed;
            }
            if(speed <= -maxAngularSpeed) {
                tmpSpeed = -maxAngularSpeed;
            }
            this.transform.RotateAround(this.target.transform.TransformPoint(this.target.GetComponent<BoxCollider>().center), Vector3.up, tmpSpeed);
        }
    }

    public void Enable(GameObject go, bool enable) {
        ITargetAnswer answer = go.GetComponent<ITargetAnswer>();
        if(answer != null) {
            answer.OnActive(enable);
        }
    }
}
