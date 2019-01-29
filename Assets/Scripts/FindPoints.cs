using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPoints : MonoBehaviour 
{
	private BoxCollider col;
	private Material[] mats;

	private Vector3 vertice1;
	private Vector3 vertice2;
	private Vector3 vertice3;
	private Vector3 vertice4;
	private Vector3 vertice5;
	private Vector3 vertice6;
	private Vector3 vertice7;
	private Vector3 vertice8;

	private Vector3[] boxColliderVertices;
	private Vector3[] onScreenVertices;
	private bool[] pointsOut;

	private Color[] baseColor;
	public float alphaLevels;

	public bool isOut;


	void Start () 
	{
		col = GetComponent<BoxCollider> ();
		mats = GetComponent<MeshRenderer> ().materials;
		onScreenVertices = new Vector3[8];
		pointsOut = new bool[8];
		baseColor = new Color[mats.Length];
		alphaLevels = 1f;


		for (int i =0;i< mats.Length;i++)
		{
			baseColor [i] = mats [i].color;
		}


		//Get the real world coordinates of the bounding box
		vertice1 = transform.TransformPoint (col.center + new Vector3 (-col.size.x, -col.size.y, -col.size.z) * 0.5f);
		vertice2 = transform.TransformPoint (col.center + new Vector3 (col.size.x, -col.size.y, -col.size.z) * 0.5f);
		vertice3 = transform.TransformPoint (col.center + new Vector3 (col.size.x, -col.size.y, col.size.z) * 0.5f);
		vertice4 = transform.TransformPoint (col.center + new Vector3 (-col.size.x, -col.size.y, col.size.z) * 0.5f);
		vertice5 = transform.TransformPoint (col.center + new Vector3 (-col.size.x, col.size.y, -col.size.z) * 0.5f);
		vertice6 = transform.TransformPoint (col.center + new Vector3 (col.size.x, col.size.y, -col.size.z) * 0.5f);
		vertice7 = transform.TransformPoint (col.center + new Vector3 (col.size.x, col.size.y, col.size.z) * 0.5f);
		vertice8 = transform.TransformPoint (col.center + new Vector3 (-col.size.x, col.size.y, col.size.z) * 0.5f);

		//Put those value in  an array
		boxColliderVertices = new Vector3[8];
		boxColliderVertices [0] = vertice1;
		boxColliderVertices [1] = vertice2;
		boxColliderVertices [2] = vertice3;
		boxColliderVertices [3] = vertice4;
		boxColliderVertices [4] = vertice5;
		boxColliderVertices [5] = vertice6;
		boxColliderVertices [6] = vertice7;
		boxColliderVertices [7] = vertice8;


	}
	
	void Update () 
	{

		//Assign real world vertices to viewport coordinates vertices
		for(int i=0 ; i<=7;i++)
		{
			onScreenVertices [i] = Camera.main.WorldToViewportPoint (boxColliderVertices [i]);
		}

		//Check if one vertice is out of screen and assign true to boolean
		for(int i=0 ; i<=7;i++)
		{
			if(onScreenVertices[i].x<0 || onScreenVertices[i].x>1 || onScreenVertices[i].y<0|| onScreenVertices[i].y>1)
			{
				isOut=true;
				pointsOut [i] = true;
			}
			else
			{
				pointsOut [i] = false;
			}


			if (pointsOut[i]==true)
			{
				isOut = true;
				break;

			}
			else if (pointsOut[i]==false)
			{
				isOut=false;

			}
		}


		//Set alpha Levels
		if(isOut==false )
		{
			alphaLevels= alphaLevels + 0.1f;
		}
		if (isOut==true)
		{
			alphaLevels= alphaLevels - 0.1f;
		}

		alphaLevels = Mathf.Clamp (alphaLevels, 0, 1);


		//Change all materials's alpha
		for (int i =0;i< mats.Length;i++)
		{
			mats [i].color = new Color (baseColor [i].r, baseColor [i].g, baseColor [i].b, alphaLevels);
		}
			

	}
}
