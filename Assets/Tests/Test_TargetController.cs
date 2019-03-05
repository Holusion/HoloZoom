using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_TargetController
    {
        GameObject CreateSelectableObject(string name, Vector3 position) {
            GameObject obj = new GameObject(name);
            // obj.layer = LayerMask.NameToLayer("Selectable");
            obj.transform.position = position;

            obj.AddComponent<BoxCollider>().center = new Vector3(0.5f, 0.5f , 0.5f);
            obj.AddComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f , 0.5f);

            return obj;
        }

        TargetController CreateTargetController(Vector3 initPos) {
            TargetController tracker = new GameObject().AddComponent<TargetController>();
            tracker.Construct();

            tracker.transform.position = initPos;
            return tracker;
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerSetTargetNonSelectableObject()
        {
            yield return null;   
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerSetTargetWithSelectableObject()
        {
            GameObject initPos = new GameObject("InitPos");
            initPos.transform.position = new Vector3(0, 0, -1);

            GameObject obj1 = CreateSelectableObject("Cube1", new Vector3(1, 1, 1));

            Activator initPosActivator = initPos.AddComponent<Activator>();
            initPosActivator.nextSelectable.Add(new Activator.ActivatorObject(obj1, true));

            TargetController targetController = CreateTargetController(initPos.transform.position);
            targetController.SetTarget(obj1);

            yield return null;

            Assert.AreNotEqual(targetController.transform.position, initPos.transform.position);
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerRotateOnInitPos()
        {
            yield return null;   
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerRotateOnSelectableObject()
        {
            yield return null;   
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerResetWhenNoPreviousGameObject()
        {
            Vector3 initPos = new Vector3(0, 0, 0);
            TargetController tracker = CreateTargetController(initPos);
            tracker.Reset();

            yield return null;

            Assert.AreEqual(initPos, tracker.transform.position);   
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerResetWithPreviousGameObject()
        {
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerEnableGameObject()
        {
            yield return null;   
        }

        [TearDown]
        public void AfterAll() {
            foreach(Object gameObject in Object.FindObjectsOfType<TargetController>()) {
                Object.Destroy(gameObject);
            }
        }
    }
}
