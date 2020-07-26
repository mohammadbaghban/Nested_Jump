
using UnityEngine;

public class Bubble : MonoBehaviour
{
	private float first;
	// Use this for initialization
	void Start ()
	{
		first = System.DateTime.Now.Second;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float second = System.DateTime.Now.Second - first;
		if (second > 28)
		{
			Destroy(gameObject);			
		}
	}
}
