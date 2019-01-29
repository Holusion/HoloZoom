using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
	public class ClickOn : MonoBehaviour
	{
		public bool isSelected = false;

		void Update () 
		{
			if (isSelected==true)
			{
				GetComponent<Outline>().eraseRenderer =false;
			}
			else
			{
				GetComponent<Outline> ().eraseRenderer = true;
			}
		}



	}



}





