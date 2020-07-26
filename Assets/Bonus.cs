using UnityEngine;

public class Bonus : MonoBehaviour
{
	public Material material;
	private int randNumber;

	public GameObject bubble;
	// Use this for initialization
	void Start () {
		randNumber = Random.Range(40, 80);
		if (randNumber % 2 == 1)
		{
			randNumber *= -1;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(
			Time.deltaTime * randNumber,
			Time.deltaTime * randNumber,
			Time.deltaTime * randNumber
		);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Ball")
		{
			Vector3 bubblePos = gameObject.transform.position;
			AudioSource sound = GameObject.Find("Blop Sound").GetComponent<AudioSource>();
			sound.Play();
		

			for (int i = 0; i < 20; i++)
			{
				GameObject newBubble = Instantiate(bubble, bubblePos, Quaternion.identity);
				newBubble.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-3f, 3f),Random.Range(-3f, 3f), Random.Range(-2f, +2f));
				newBubble.GetComponent<MeshRenderer>().material.color =
					gameObject.GetComponent<MeshRenderer>().material.color;
				newBubble.GetComponent<MeshRenderer>().material.color = new Color(newBubble.GetComponent<MeshRenderer>().material.color.r
				, newBubble.GetComponent<MeshRenderer>().material.color.g, newBubble.GetComponent<MeshRenderer>().material.color.b, 0.4f);
			}
			Destroy(gameObject);
		}
	}
}
