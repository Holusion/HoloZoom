using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetController : NetworkBehaviour {

    public GameObject interactiveTarget;
    public GameObject target;
    public float rotationSpeed = 0.1f;
    public float zoomSpeed = 5f;
    public float maxAngularSpeed = 10f;
    public bool targetChange = false;
    Quaternion initRotation;
    Stack<IDoUndo> lastTarget;
    public GameObject initPos;
    public bool readyToRotate = false;

    public void Construct() {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        this.initRotation = transform.rotation;
        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
        this.target = initPos;
        this.lastTarget = new Stack<IDoUndo>();
    }

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
        this.target = initPos;
        this.lastTarget = new Stack<IDoUndo>();
	}
	
	// Update is called once per frame
	void Update () {
        if(targetChange)
        {
            Vector3 direction;
            Vector3 targetPosition;
            Quaternion toRotation;
            float targetFieldView = 60;
            float targetFarPlane = 8;

            if(target.GetComponent<BoxCollider>() != null) {
                direction = target.GetComponent<BoxCollider>().bounds.center - transform.position;
                targetPosition = target.transform.Find("CamPos").transform.position;
                toRotation = Quaternion.LookRotation(direction);
            } else {
                targetPosition = target.transform.position;
                toRotation = initRotation;
                targetFieldView = 8;
                targetFarPlane = 500;
            }

            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this.readyToRotate = false;

            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.Lerp(transform.position,targetPosition, zoomSpeed * Time.deltaTime);
            Camera.main.fieldOfView += (targetFieldView - Camera.main.fieldOfView) / (distance + 1);
            Camera.main.farClipPlane += (targetFarPlane - Camera.main.farClipPlane) / (distance + 1);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1 && Quaternion.Angle(toRotation, transform.rotation) < 1)
            {
                targetChange = false;
                this.readyToRotate = true;
            }
        }
    }

    public void SetTarget(GameObject go)
    {
        IDoUndo command = new CommandSelection(this, go, this.target);
        lastTarget.Push(command);
        command.Do();
    }

    public void Reset()
    {
        if(this.lastTarget.Count > 0) {
            lastTarget.Pop().Undo();
        }
    }

    public void RotateTarget(float speed)
    {
        float tmpSpeed = speed;
        Vector3 rotateAroundPoint = new Vector3();

        if(this.target.GetComponent<BoxCollider>() != null) {
            rotateAroundPoint = this.target.transform.TransformPoint(this.target.GetComponent<BoxCollider>().center);
            ITargetAnswer[] answers = target.GetComponents<ITargetAnswer>();
            foreach(ITargetAnswer answer in answers) {
                if(answer != null) {
                    answer.OnRotate();
                }
            }
        } else {
            foreach(Activator.ActivatorObject ao in this.target.GetComponent<Activator>().nextSelectable) {
                rotateAroundPoint += ao.gameObject.transform.position;
            }
            rotateAroundPoint /= this.target.GetComponent<Activator>().nextSelectable.Count;
        }

        if(speed >= maxAngularSpeed) {
            tmpSpeed = maxAngularSpeed;
        }
        if(speed <= -maxAngularSpeed) {
            tmpSpeed = -maxAngularSpeed;
        }
        this.transform.RotateAround(rotateAroundPoint, Vector3.up, tmpSpeed);

    }

    public void Enable(GameObject go, bool enable) {
        ITargetAnswer[] answers = go.GetComponents<ITargetAnswer>();
        foreach(ITargetAnswer answer in answers) {
            if(answer != null) {
                answer.OnActive(enable);
            }
        }
    }

    public void Animate(GameObject go, string triggerOn, string triggerOff, bool shouldStack) {
        IDoUndo command = new CommandAnimation(go, triggerOn, triggerOff, shouldStack);
        if(shouldStack) lastTarget.Push(command);
        command.Do();
    }
}
