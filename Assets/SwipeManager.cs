using System;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
	private bool mouseDown = false;
	private Vector3 currentPos;
	private Vector3 lastPos = new Vector3(-1, -1, -1);
	private Vector3 firstPos = new Vector3(-1, -1, -1);
	private Vector3 posDifferent;
	private Vector3 posDifferent2;
	private Vector3 posDifferent3;
	public GameObject currentShape;
	public GameObject ball;


	// Use this for initialization
	void Start()
	{
		currentShape = GameObject.Find("1");
		//currentShape2 = GameObject.Find("2");
		//camera = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseDown = true;
			var mousePos = Input.mousePosition;
			mousePos.z = UnityEngine.Camera.main.nearClipPlane;
			firstPos = UnityEngine.Camera.main.ScreenToWorldPoint(mousePos);
			//firstPos3 = UnityEngine.Camera.main.ScreenToWorldPoint(mousePos);
		}

		if (mouseDown)
		{
			var mousePos = Input.mousePosition;
			
			mousePos.z = UnityEngine.Camera.main.nearClipPlane;
			currentPos = UnityEngine.Camera.main.ScreenToWorldPoint(mousePos);
			if (lastPos != new Vector3(-1, -1, -1))
			{
				posDifferent = currentPos - lastPos;
			}

			if (firstPos != new Vector3(-1, -1, -1))
			{
				posDifferent2 = currentPos - firstPos;
			}


			lastPos = currentPos;

			if (currentShape){
				//Debug.Log("current pos" + currentPos.y + "\n shape pos" + currentShape.transform.position.y);
			if (currentPos.y < currentShape.transform.position.y - 1.4)
			{
				if (currentShape.transform.GetChild(0).name.Contains("Circle"))
				{
					currentShape.transform.Rotate(0, 0, posDifferent.x * 1800);
				}
				else if (currentShape.transform.GetChild(0).name.Contains("Polygon"))
				{
					if (Math.Abs(posDifferent2.x) > 0.02)
					{
						currentShape.transform.Rotate(0f, 0f, posDifferent2.x / Math.Abs(posDifferent2.x) * 30);
						firstPos = currentPos;
					}
				}
				else if (currentShape.transform.GetChild(0).name.Contains("Square"))
				{
					if (Math.Abs(posDifferent2.x) > 0.022)
					{
						currentShape.transform.Rotate(0f, 0f, posDifferent2.x / Math.Abs(posDifferent2.x) * 45);
						firstPos = currentPos;
					}
				}
			}
			else
			{
				if (currentShape.transform.GetChild(0).name.Contains("Circle"))
				{
					currentShape.transform.Rotate(0, 0, -posDifferent.x * 1800);
				}
				else if (currentShape.transform.GetChild(0).name.Contains("Polygon"))
				{

					if (Math.Abs(posDifferent2.x) > 0.02)
					{
						currentShape.transform.Rotate(0f, 0f, -posDifferent2.x / Math.Abs(posDifferent2.x) * 30);
						firstPos = currentPos;
					}
				}
				else if (currentShape.transform.GetChild(0).name.Contains("Square"))
				{
					if (Math.Abs(posDifferent2.x) > 0.022)
					{
						currentShape.transform.Rotate(0f, 0f, -posDifferent2.x / Math.Abs(posDifferent2.x) * 45);
						firstPos = currentPos;
					}
				}
			}

		}

			if (Input.GetMouseButtonUp(0))
			{
				mouseDown = false;
				lastPos = new Vector3(-1, -1, -1);
				firstPos = new Vector3(-1, -1 - -1);
			}

			currentShape = GameObject.Find((ball.GetComponent<BallScript>().level).ToString());
		}

	}
}
