using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    const string BUTTON_LEFT = "Fire1";
    const string BUTTON_RIGHT = "Fire2";
    const string SELECT = "select";
    const string UNSELECT = "unselect";
    const string ROTATE = "rotate";

    private float lastSpeed = 0;

    void Update() {
        if(isClient) {
            float speed = Input.GetAxisRaw("Mouse X");

            if(Input.GetButtonDown(BUTTON_LEFT)) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1))
                {
                    CmdTarget(SELECT, hit.collider.gameObject.name);
                } else {
                    CmdTarget(UNSELECT, "");
                }
            } 
            else if(Input.GetButtonDown(BUTTON_RIGHT)) 
            {
                CmdTarget(UNSELECT, "");
            } 
            else if(Input.GetButton(BUTTON_LEFT) && speed != 0)
            {
                CmdRotate(speed);
                lastSpeed = speed;
            } else {
                if(lastSpeed >= 0.001f || lastSpeed <= -0.001f) {
                    lastSpeed /= 1.1f;
                    CmdRotate(lastSpeed);
                }
            }
        }
    }

    [Command]
    public void CmdTarget(string interaction, string hit) {
        RpcTarget(interaction, hit);
    }

    [Command]
    public void CmdRotate(float speed) {
        RpcRotate(speed);
    }

    [ClientRpc]
    public void RpcTarget(string interaction, string hit) {
        GameObject go = GameObject.Find(hit);
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();

        if(interaction == SELECT) {
            controller.stateMachine.Step("select", () => controller.SetTarget(go));
        } else if (interaction == UNSELECT) {
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
}