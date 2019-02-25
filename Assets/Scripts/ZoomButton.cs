using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace cakeslice

{
    public class ZoomButton : MonoBehaviour
    {
        public Button roomButton;
        public Button backButton;

        public bool isClicked;

        public GameObject myRoom;


        void Start()
        {
            isClicked = false;
            GetComponentInChildren<Text>().text = "Zoom on " + myRoom.name;
        }


        void Update()
        {

            if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                isClicked = true;
                
            }
            else if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject != gameObject)
            {
                isClicked = false;
                roomButton.gameObject.SetActive(true);
                gameObject.SetActive(false);

            }

            if (isClicked == true)
            {
                myRoom.GetComponent<ClickOn>().IsChoosen();
                backButton.gameObject.SetActive(true);

            }
            else
            {
                myRoom.GetComponent<ClickOn>().NotChoosen();
            }
                



        }
    }
}
