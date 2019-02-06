using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace cakeslice
{
	public class Click : MonoBehaviour 
	{
		private List<GameObject> selectedObjects;
		private Input click;

        // public NetworkClientBehaviour network;


		// Use this for initialization
		void Start () 
		{
			selectedObjects = new List<GameObject> ();
			#if UNITY_EDITOR
			Debug.Log("Unity Editor");
			#endif

			#if UNITY_IOS || UNITY_ANDROID
			Debug.Log("Mobile");
			#endif
		}

		// Update is called once per frame
		void Update () 
		{
			
			if (Input.GetButton("Fire1"))
			{
				RaycastHit rayhit;

				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhit,Mathf.Infinity)&& rayhit.transform.gameObject.tag=="Selectable")
				{
					
					if(selectedObjects.Count >0)
					{
						foreach (GameObject obj in selectedObjects)
						{
							obj.GetComponent<ClickOn> ().isSelected = false;
						}
						selectedObjects.Clear ();

					}

					selectedObjects.Add (rayhit.collider.gameObject);

					rayhit.collider.GetComponent<ClickOn> ().isSelected = true;
				}
				else
				{
					if(selectedObjects.Count >0)
					{
						foreach (GameObject obj in selectedObjects)
						{
							obj.GetComponent<ClickOn> ().isSelected = false;
						}
						selectedObjects.Clear ();
					}
				}
			}


			#if UNITY_IOS  || UNITY_ANDROID
			if(Input.touchCount >0  && Input.GetTouch(0).phase==TouchPhase.Began)
			{
				RaycastHit rayhit;

				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhit,Mathf.Infinity)&& rayhit.transform.gameObject.tag=="Selectable")
				{
					if(selectedObjects.Count >0)
					{
						foreach (GameObject obj in selectedObjects)
						{
							obj.GetComponent<ClickOn> ().isSelected = false;
						}
						selectedObjects.Clear ();

					}

					selectedObjects.Add (rayhit.collider.gameObject);

					rayhit.collider.GetComponent<ClickOn> ().isSelected = true;
                }
				else
				{
					if(selectedObjects.Count >0)
					{
						foreach (GameObject obj in selectedObjects)
						{
							obj.GetComponent<ClickOn> ().isSelected = false;
						}
						selectedObjects.Clear ();
                    }
				}
			}
			#endif







		}
	}
}


