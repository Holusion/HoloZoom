using UnityEngine;
using UnityEngine.Networking;

public class MyMsgType {
    public static short State = MsgType.Highest + 1;
}

public class StateMessage : MessageBase {
    public byte[] payload;
}