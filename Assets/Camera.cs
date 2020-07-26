
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	private GameObject ball;

	public int level;
	public List<GameObject> destroyShapes;
	Vector3 pos = new Vector3(0, 0, 0);

	public Vector3 lastPos;
	public Vector3 newPos;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");
		lastPos = transform.position;
		//GetComponent<UnityEngine.Camera>().backgroundColor = HexToColor(backGroundColors[rand]);
	}
	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
	
	// Update is called once per frame
	void Update ()
	{
		int level2 = level;
		level = ball.GetComponent<BallScript>().level;
		if (level != level2)
		{
			int rand = Random.Range(0, 9);
			//GetComponent<UnityEngine.Camera>().backgroundColor = HexToColor(backGroundColors[rand]);

			if (GameObject.Find((ball.GetComponent<BallScript>().level).ToString()))
			{
				ball.GetComponent<MeshRenderer>().material.color = GameObject
					.Find((ball.GetComponent<BallScript>().level).ToString()).transform.GetChild(0)
					.GetComponent<MeshRenderer>().material.color;
			}
		}

		if (ball.transform.position.y < 0 && -ball.transform.position.y % 10 > 3.5 && -ball.transform.position.y % 10 < 4.5)
		{
			
			GetComponent<Rigidbody>().velocity = new Vector3(0, -20, 0);
			ShapeDestroy();
		}
		else if ((-transform.position.y - 1.5f) % 10 <= 0.6)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			transform.position = new Vector3(transform.position.x, (level - 1) * -10 - 1.5f, transform.position.z);
		}

	
	}

	void ShapeDestroy()
	{
		var currentSape = GameObject.Find((ball.GetComponent<BallScript>().level).ToString());

		for (int i = 0; i < 20; i++)
		{
			if (currentSape)
			{
				pos = currentSape.transform.position;
				int rand = Random.Range(0, 8);
				switch (rand)
				{
						case 0: pos.x += 2;
							pos.y += 0;
							break;
						case 1: pos.x += 2;
							pos.y += 2;
							break;
						case 2: pos.x += 0;
							pos.y += 2;
							break;
						case 3: pos.x += -2;
							pos.y += 2;
							break;
						case 4: pos.x += -2;
							pos.y += 0;
							break;
						case 5: pos.x += -2;
							pos.y += -2;
							break;
						case 6: pos.x += 0;
							pos.y += -2;
							break;
						case 7: pos.x += 2;
							pos.y += -2;
							break;
				}
				//pos.x += Random.Range(0, 2) * 4 - 2;
				//pos.y += Random.Range(0, 2) * 4 - 2;
				GameObject newShape = Instantiate(destroyShapes[Random.Range(0, 4)], pos, Quaternion.identity);
				float speedX = Random.Range(1f, 20f);
				float speedY = Random.Range(1f, 20f);
				if (pos.x < 0)
				{
					speedX *= -1;
				}

				if (pos.y < currentSape.transform.position.y)
				{
					speedY *= -1;
				}
				newShape.GetComponent<Rigidbody>().velocity = new Vector3(speedX, speedY, Random.Range(-4f, 4f));
				newShape.GetComponent<Rigidbody>().AddTorque(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
				if (Random.RandomRange(0, 3) == 0)
				{
					newShape.GetComponent<MeshRenderer>().material.color = Color.red;
				}
				else
				{
					newShape.GetComponent<MeshRenderer>().material.color =
						currentSape.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
				}
			}
			
		}

		if (currentSape)
		{
			Destroy(currentSape);
		}


	}
}
