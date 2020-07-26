using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
	private Rigidbody rb;
	public GameObject GameOverPanel;
	public GameObject SwipeManager;
	public GameObject PassedPanel;
	public GameObject bubble;
	public int numberOfBonuses = 0;
	public const int SPEED = 10;
	private bool lastBreaked = false;
	private float start = -1;
	private int mainLevel = 1;

	public int level = 1;
	
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, SPEED, 0);
		mainLevel = PlayerPrefs.GetInt("mainLevel", 1);
		//rb.AddForce(new Vector3(0, 15f, 0), ForceMode.Impulse);
		//rb.AddTorque(20f, 20f, 0);

		//PlayerPrefs.DeleteAll();
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.gameObject.name);
		if (other.gameObject.name.Contains("Main"))
		{
			AudioSource sound = gameObject.GetComponent<AudioSource>();
			sound.Play();
			//Debug.Log(rb.velocity.y);
			if (Math.Abs(rb.velocity.y) > 25f && !lastBreaked)
			{
				UnityEngine.Camera.main.GetComponent<Camera>().Invoke("ShapeDestroy", 0f);
				Destroy(other.gameObject.transform.parent.gameObject);
				lastBreaked = true;
			}
			else
			{
				lastBreaked = false;
			}
			rb.velocity = new Vector3(0, SPEED, 0);
			
			//rb.AddForce(new Vector3(0, 30f, 0), ForceMode.Impulse);
			//float randNumber = UnityEngine.Random.Range(-180f, 180f);
		//	rb.AddTorque(randNumber, randNumber, randNumber);
		}
		else if (other.gameObject.name.Contains("Red"))
		{
			AudioSource sound = gameObject.GetComponent<AudioSource>();
			sound.Play();
			
			GameOverPanel.SetActive(true);
			if (GameObject.Find("LevelText"))
			{
				GameObject.Find("LevelText").SetActive(false);
			}

			Destroy(SwipeManager);
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		} else if (other.gameObject.name.Contains("Bonus"))
		{
			
			numberOfBonuses++;
			if (GameObject.Find("LevelText"))
			{
				GameObject.Find("LevelText").GetComponent<Text>().text = numberOfBonuses.ToString();
			}
			//Debug.Log(numberOfBonuses);

			if (numberOfBonuses == mainLevel * 2 - 1)
			{
				UnityEngine.Camera.main.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			} else if (numberOfBonuses == mainLevel * 2)
			{
				numberOfBonuses = 0;
				level = 0;
				rb.constraints = RigidbodyConstraints.FreezeAll;
				PassedPanel.SetActive(true);
				GameObject.Find("LevelText").SetActive(false);
				PassedPanel.transform.GetChild(0).GetComponent<Text>().text =  (mainLevel + 1) + " ﯼﻪﻠﺣﺮﻣ";
				PlayerPrefs.SetInt("mainLevel", mainLevel + 1);
				start = DateTime.Now.Second;
				//Debug.Log("Start" + start);
			}
		}

	}


	// Update is called once per frame
	void Update ()
	{
		level = ((int)Math.Round(-transform.position.y) + 5) / 10 + 1;
		//Debug.Log("Time" + System.DateTime.Now.Second);
		//Debug.Log("main level" + mainLevel);
		if (Math.Abs(System.DateTime.Now.Second - start) > 1.5f && start > -1)
		{
			SceneManager.LoadScene(0);
		}
		
	}
}
