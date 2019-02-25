using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class WallHack : MonoBehaviour
    {
        public GameObject decoy;
        RaycastHit rayhit;
        private Ray ray;
        private int layerMask = ~(1 << 8);




        void Start()
        {

            decoy = gameObject.transform.Find("Decoy").gameObject;

            
            ray = new Ray(Camera.main.transform.position, decoy.transform.position - Camera.main.transform.position);
            
    
            
        }



        void Update()
        {
            
            ray = new Ray(Camera.main.transform.position, (decoy.transform.position - Camera.main.transform.position));


             if (gameObject.GetComponentInParent<ClickOn>().isChoosen == true)
            {

                if (Physics.Raycast(ray, out rayhit, 100, layerMask))
                {
                   
                    Debug.DrawRay(Camera.main.transform.position, (decoy.transform.position - Camera.main.transform.position), Color.cyan);
                    //Debug.Log(rayhit.transform.gameObject.name);




                    if (rayhit.transform.gameObject != decoy && rayhit.transform.gameObject == transform.gameObject)
                    {

                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;



                    }
                    else
                    {
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }          
                
                
            }


             if (gameObject.GetComponent<BoxCollider>().enabled==false)
            {
                decoy.GetComponent<BoxCollider>().enabled = false;
            }
            if (gameObject.GetComponent<BoxCollider>().enabled == true)
            {
                decoy.GetComponent<BoxCollider>().enabled = true;
            }

        }
    }

}
