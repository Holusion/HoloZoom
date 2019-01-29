using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OverrideNetworkDiscovery : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();
        this.StopBroadcast();
    }

    public void CreateClient() {
        this.Initialize();
        this.StartAsClient();
    }

    void Start() {
        this.showGUI = false;
    }
}
