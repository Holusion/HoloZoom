using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_TargetController
    {
        [UnityTest]
        public IEnumerator Test_TargetControllerWithEnumeratorPasses()
        {
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerSetTargetNonSelectableObject()
        {
            yield return null;   
        }

        [UnityTest]
        public IEnumerator Test_TargetControllerSetTargetWithSelectableObject()
        {
            yield return null;   
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
            TargetController tracker = new GameObject().AddComponent<TargetController>();
            tracker.Construct();

            Vector3 initPos = new Vector3(0, 0, 0);
            tracker.transform.position = initPos;
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
