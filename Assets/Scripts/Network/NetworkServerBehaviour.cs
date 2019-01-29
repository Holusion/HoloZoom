using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkServerBehaviour : MonoBehaviour
{
    private void Start() {
        NetworkManager.singleton.StartHost();
    }
}
