using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour {

    public const string BUTTON_LEFT = "Fire1";
    public const string BUTTON_RIGHT = "Fire2";
    public const string SELECT = "select";
    public const string UNSELECT = "unselect";
    public const string ROTATE = "rotate";

    private float lastSpeed = 0;

    private InteractionManager interactionManager;

    void Start() {
        this.interactionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
    }

    void Update() {
        this.interactionManager.UpdateInteractions(this);
    }

    public bool IsUI() {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    [Command]
    public void CmdTarget(string action, GameObject hit) {
        RpcTarget(action, hit);
    }

    [Command]
    public void CmdRotate(float speed) {
        RpcRotate(speed);
    }

    [Command]
    public void CmdEnable(GameObject go, bool enable) {
        RpcEnable(go, enable);
    }

    [ClientRpc]
    public void RpcTarget(string action, GameObject hit) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();

        if(action == SELECT) {
            controller.stateMachine.Step("select", () => controller.SetTarget(hit));
        } else if (action == UNSELECT) {
            controller.stateMachine.Step("unselect", () => controller.SetTarget(controller.initPos, true));
        }
    }

    [ClientRpc]
    public void RpcRotate(float speed) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        if(controller.readyToRotate) {
            controller.stateMachine.Step("rotate", () => controller.RotateTarget(speed));
        }
    }

    [ClientRpc]
    public void RpcEnable(GameObject go, bool enable) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.FadeOne(go, enable);
    }
}