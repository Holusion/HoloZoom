using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace cakeslice
{
    public class RoomButton : MonoBehaviour
    {
        public GameObject myRoom;
        public bool isClicked;

        public Button zoomButton;
        
        

        void Start()
        {
            GetComponentInChildren<Text>().text = "View " + myRoom.name;
            isClicked = false;
            
            
        }



        void Update()
        {
            if(Input.GetMouseButton(0)&& EventSystem.current.currentSelectedGameObject==gameObject)
            {
                isClicked = true;
                myRoom.GetComponent<ClickOn>().isSelected = true;
            }
            else if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject != gameObject)
            {
                isClicked = false;
                myRoom.GetComponent<ClickOn>().isSelected = false;
            }


            if (isClicked == true)
            {
                GetComponentInChildren<Text>().text = "Zoom on " + myRoom.name;
                gameObject.SetActive(false);
                zoomButton.gameObject.SetActive(true);
               
                
                

            }
            else
            {
                GetComponentInChildren<Text>().text = "View " + myRoom.name;


            }    



        }


       



        



    }

}

