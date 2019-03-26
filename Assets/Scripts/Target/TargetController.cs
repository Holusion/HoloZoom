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
    public CommandList lastTarget;
    public GameObject initPos;
    public bool readyToRotate = true;
    public float timeBeforeStandBy = 5.0f;
    float currentTime;
    bool standByLauch = false;
    CameraBehaviour cameraBehaviour;
    public GameObject map;

    public void Construct() {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        this.initRotation = transform.rotation;
        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
        this.target = initPos;
        this.lastTarget = new CommandList();
    }

    // Use this for initialization
    void Start () {
        this.initRotation = transform.rotation;
        this.initPos = GameObject.Find("InitPos");
        this.target = initPos;
        this.lastTarget = new CommandList();

        this.currentTime = Time.time; 
        this.cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if(targetChange)
        {
            Vector3 direction;
            Vector3 targetPosition;
            Quaternion toRotation;
            float targetFieldView = cameraBehaviour.maxFOV;
            float targetFarPlane = cameraBehaviour.minFarClip;

            if(target.GetComponent<BoxCollider>() != null) {
                direction = target.GetComponent<BoxCollider>().bounds.center - transform.position;
                targetPosition = target.transform.Find("CamPos").transform.position;
                toRotation = Quaternion.LookRotation(direction);
            } else {
                targetPosition = cameraBehaviour.initPos;
                toRotation = cameraBehaviour.initRot;
                targetFieldView = cameraBehaviour.minFOV;
                targetFarPlane = cameraBehaviour.maxFarClip;
            }

            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this.readyToRotate = false;

            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.Lerp(transform.position,targetPosition, zoomSpeed * Time.deltaTime);
            cameraBehaviour.ChangeFOV(targetFieldView, distance);
            cameraBehaviour.ChangeFarClip(targetFarPlane, distance);

            if(this.isServer) this.map.SetActive(this.target == initPos);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01 && Quaternion.Angle(toRotation, transform.rotation) < 1)
            {
                targetChange = false;
                this.readyToRotate = true;
            }
            this.currentTime = Time.time;
        } else if (!standByLauch && Time.time >= currentTime + timeBeforeStandBy && !this.isServer) {
            standByLauch = true;
            StartCoroutine("StandByLoop");
        }
    }

    public void SetTarget(GameObject go)
    {
        Command command = new CommandSelection(this, go, this.target);
        lastTarget.Push(command);
        this.currentTime = Time.time;
    }

    public void Reset()
    {
        if(this.lastTarget.Count() > 0) {
            lastTarget.Pop();
        }
        this.currentTime = Time.time;
    }

    public void RotateTarget(float speed)
    {
        if(readyToRotate) {
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
                rotateAroundPoint += this.target.transform.position;
            }

            if(speed >= maxAngularSpeed) {
                tmpSpeed = maxAngularSpeed;
            }
            if(speed <= -maxAngularSpeed) {
                tmpSpeed = -maxAngularSpeed;
            }
            this.transform.RotateAround(rotateAroundPoint, Vector3.up, tmpSpeed);
            this.currentTime = Time.time;
        }
    }

    public void Enable(GameObject go, bool enable) {
        ITargetAnswer[] answers = go.GetComponents<ITargetAnswer>();
        foreach(ITargetAnswer answer in answers) {
            if(answer != null) {
                answer.OnActive(enable);
            }
        }
        this.currentTime = Time.time;
    }

    public void Animate(GameObject go, string triggerOn, string triggerOff, bool shouldStack) {
        Command command = new CommandAnimation(go, triggerOn, triggerOff, shouldStack);
        if(shouldStack) lastTarget.Push(command);
        else command.Do();
        this.currentTime = Time.time;
    }

    public void WakeUp() {
        if(this.standByLauch) {
            this.StopCoroutine("StandByLoop");
            while(this.lastTarget.Count() > 0) {
                Reset();
            }
            this.standByLauch = false;
            this.transform.position = cameraBehaviour.initPos;
            this.transform.rotation = cameraBehaviour.initRot;
        }
    }

    IEnumerator StandByLoop() {
        while(this.standByLauch) {
            while(this.lastTarget.Count() > 0) {
                this.Reset();
            }

            for(int j = 0; j < initPos.GetComponent<Activator>().nextSelectable.Count; j++) {
                for(int i = 0; i < 360 * 2; i++) {
                    RotateTarget(0.5f);
                    yield return new WaitForSeconds(.01f);
                }

                SetTarget(initPos.GetComponent<Activator>().nextSelectable[j].gameObject);
                yield return new WaitForSeconds(2f);

                for(int i = 0; i < 360 * 2; i++) {
                    RotateTarget(0.5f);
                    yield return new WaitForSeconds(.01f);
                }

                Reset();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public override bool OnSerialize(NetworkWriter writer, bool initialState) {
        if(initialState) {
            
        }

        return false;
    }
}
