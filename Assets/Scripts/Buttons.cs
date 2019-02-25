using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cakeslice

{

public class Buttons : MonoBehaviour
{
    public GameObject[] selectables;    
    public bool noChoosen;
    public bool noSelection;

    public GameObject mainBat;

    public GameObject canvas;
    public Button button;
    public Button zoomButton;
    public Button backButton;


    private float x;
    private float y;
    private float centerY;
    private float centerX;


        private void Awake()
        {
            x = canvas.GetComponent<RectTransform>().rect.width;
            y = canvas.GetComponent<RectTransform>().rect.height;
            centerY = button.GetComponent<RectTransform>().rect.height / 2;
            centerX= button.GetComponent<RectTransform>().rect.width / 2;

            selectables = GameObject.FindGameObjectsWithTag("Selectable");
            
            noChoosen = true;
            noSelection = true;

            Button newBackButton = Instantiate(backButton) as Button;
            newBackButton.transform.SetParent(canvas.transform, false);
            newBackButton.transform.position = new Vector3(x - centerX, y - (y-centerY*2), 0);



            for (int i = 0; i < selectables.Length; i++)
            {
                
                Button newButton = Instantiate(button) as Button;
                newButton.transform.SetParent(canvas.transform, false);
                newButton.GetComponent<RoomButton>().myRoom = selectables[i];
                newButton.transform.position = new Vector3(x - centerX , y - centerY - 2 * centerY * i, 0);

                Button newZoomButton = Instantiate(zoomButton) as Button;
                newZoomButton.transform.SetParent(canvas.transform, false);
                newZoomButton.GetComponent<ZoomButton>().myRoom = selectables[i];
                newZoomButton.transform.position = new Vector3(x - centerX, y - centerY - 2 * centerY * i, 0);
                newZoomButton.gameObject.SetActive(false);

                newButton.GetComponent<RoomButton>().zoomButton = newZoomButton;
                newZoomButton.GetComponent<ZoomButton>().backButton = newBackButton;
                newZoomButton.GetComponent<ZoomButton>().roomButton = newButton;


            }
        }

        void Start()
    {

            
        }

    
    void Update()
    {
            for (int i = 0; i < selectables.Length; i++)
            {
                if(selectables[i].GetComponent<ClickOn>().isChoosen==true)
                {
                    noChoosen = false;
                    //break;
                }
                else
                {
                    noChoosen = true;
                }


                if (selectables[i].GetComponent<ClickOn>().isSelected == true)
                {
                    noSelection = false;
                    break;
                }
                else
                {
                    noSelection = true;
                }

            }

            if(noChoosen==true)
            {
                mainBat.GetComponent<MeshRenderer>().enabled = true;
                if(mainBat.transform.childCount>0)
                {
                    foreach (Transform child in mainBat.transform)
                    {
                        child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }

            }
            else
            {
                mainBat.GetComponent<MeshRenderer>().enabled =false;
                if (mainBat.transform.childCount > 0)
                {
                    foreach (Transform child in mainBat.transform)
                    {
                        child.gameObject.GetComponent<MeshRenderer>().enabled =false;
                    }
                }
            }
            


    }
}

}
