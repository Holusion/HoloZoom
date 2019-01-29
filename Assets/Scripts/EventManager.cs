using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

    private static Dictionary<string, List<System.Action>> listeners = new Dictionary<string, List<System.Action>>();

    public static void AddEventListener(string eventName, System.Action function)
    {
        List<System.Action> list;
        if (listeners.TryGetValue(eventName, out list))
        {
            list.Add(function);
            listeners[eventName] = list;
        }
    }

    public static void DispatchEvent(string eventName, params object[] p)
    {
        List<System.Action> list;
        if(listeners.TryGetValue(eventName, out list))
        {
            foreach(System.Action function in list)
            {
                function.DynamicInvoke(p);
            }
        }
    }
}
