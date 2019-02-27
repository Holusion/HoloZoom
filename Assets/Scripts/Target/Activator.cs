using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [System.Serializable]
    public struct ActivatorObject {
        public GameObject gameObject;
        public bool initActive;
    }
    public List<ActivatorObject> nextSelectable;
}
