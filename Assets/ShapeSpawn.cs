using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShapeSpawn : MonoBehaviour
{
	public List<GameObject> shapes;
	public GameObject bonusShape;
	public GameObject bubble;

	private int mainLevel = 1;
	public int level;
	private Vector3 pos = new Vector3(0, 0, 0);

	private void Awake()
	{
		for (int i = 0; i < 30; i++)
		{
			BubbleCreator();
		}

		level = GameObject.Find("Ball").GetComponent<BallScript>().level;
		mainLevel = PlayerPrefs.GetInt("mainLevel", 1);
		
		for (int i = 0; i < mainLevel; i++)
		{
			StartCoroutine("SpawnShape");
		}
	}

	// Use this for initialization
	void Start () {
		//StartSpawning();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//int level2 = level;
		//level = GameObject.Find("Ball").GetComponent<BallScript>().level;
		//if (level != level2)
		//{
		//	StartCoroutine("SpawnShape");
		//}
		//if (GameObject.Find((level - 5).ToString())){
		//	Destroy(GameObject.Find((level - 5).ToString()));
		//}

		if (Random.Range(0, 10) == 0)
		{
			StartCoroutine("BubbleCreator");
		}
	}

	IEnumerator BubbleCreator()
	{
		float bubbleX = Random.Range(0f, 2f);
		if (bubbleX >= 1)
		{
			bubbleX = 6;
		}
		else
		{
			bubbleX = -6;
		}
		float bubbleY = Random.Range(0f, 2f);
		if (bubbleY >= 1)
		{
			bubbleY = 10 + UnityEngine.Camera.main.transform.position.y;
		}
		else
		{
			bubbleY = -10;
		}
		Vector3 bubblePos = new Vector3(bubbleX, bubbleY + UnityEngine.Camera.main.transform.position.y, 5);
		GameObject newBubble = Instantiate(bubble, bubblePos, Quaternion.identity);
		newBubble.GetComponent<Rigidbody>().velocity = new Vector3(-1f * Math.Abs(bubbleX)/bubbleX * Random.Range(0.1f, 1f), -1f * Math.Abs(bubbleY)/bubbleY * Random.Range(0.1f, 1f), Random.Range(-2f, +2f));
		int colorRand = Random.Range(0, 3);
		if (colorRand == 0)
		{
			//newBubble.GetComponent<MeshRenderer>().material.color = HexToColorBubble("00ffff");
		} else if (colorRand == 1)
		{
			//newBubble.GetComponent<MeshRenderer>().material.color = HexToColorBubble("ff33cc");
		}
		else
		{
			//newBubble.GetComponent<MeshRenderer>().material.color = HexToColorBubble("ff9933");
		}

		yield return null;
	}
	


	public void StopSpawning()
	{
		StopCoroutine("SpawnShape");
	}

	IEnumerator SpawnShape()
	{
		GameObject newShape;
		GameObject bonus1, bonus2;
		int randNumber = Random.Range(0, 7);
		int colorRand = Random.Range(0, 5);
		
		Vector3 pos2 = new Vector3(pos.x, pos.y - 5f, pos.z);
		bonus1 = Instantiate(bonusShape, pos, Quaternion.identity);
		bonus1.transform.Rotate(45, 0, 45);
		switch (colorRand)
		{
			case 0:
			{
				bonus1.GetComponent<MeshRenderer>().material.color = HexToColor("ff33cc");
				break;
			}
			case 1:
			{
				bonus1.GetComponent<MeshRenderer>().material.color = Color.cyan;
				break;
			}
			case 2:
			{
				bonus1.GetComponent<MeshRenderer>().material.color = Color.yellow;
				break;
			}
			case 3:
			{
				bonus1.GetComponent<MeshRenderer>().material.color = HexToColor("ff3300");
				break;
			}
			case 4:
			{
				bonus1.GetComponent<MeshRenderer>().material.color = HexToColor("66ff66");
				break;
			}
		}
		
		bonus2 = Instantiate(bonusShape, pos2, Quaternion.identity);
		bonus2.transform.Rotate(45, 0, 45);
		
		colorRand = Random.Range(0, 5);
		switch (colorRand)
		{
			case 0:
			{
				bonus2.GetComponent<MeshRenderer>().material.color = HexToColor("ff33cc");
				break;
			}
			case 1:
			{
				bonus2.GetComponent<MeshRenderer>().material.color = Color.cyan;
				break;
			}
			case 2:
			{
				bonus2.GetComponent<MeshRenderer>().material.color = Color.yellow;
				break;
			}
			case 3:
			{
				bonus2.GetComponent<MeshRenderer>().material.color = HexToColor("ff3300");
				break;
			}
			case 4:
			{
				bonus2.GetComponent<MeshRenderer>().material.color = HexToColor("66ff66");
				break;
			}
		}
		
		//bonus.GetComponent<Rigidbody>().AddTorque(50f, 50f, 50f);
		newShape = Instantiate(shapes[randNumber], pos, Quaternion.identity);
		if (randNumber == 0)
		{
			newShape.transform.Rotate(0, 0, 30);
		} else if (randNumber == 5)
		{
			newShape.transform.Rotate(0, 0, 20);
		}
		newShape.name = ((pos.y / -10 ) + 1).ToString();
		pos = new Vector3(0, pos.y - 10, 0);
		yield return null;
	}
	
	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}

	Color HexToColorBubble(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 70);
	}
}
