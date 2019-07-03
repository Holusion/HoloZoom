using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [System.Serializable]
    public struct ActivatorObject {
        public GameObject gameObject;
        public bool initActive;

        public ActivatorObject(GameObject gameObject, bool initActive) {
            this.gameObject = gameObject;
            this.initActive = initActive;
        }
    }
    public List<ActivatorObject> nextSelectable;
}
