using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public GameObject GuidePanel;
	
	// Use this for initialization
	void Start ()
	{
		int mainLevel = PlayerPrefs.GetInt("mainLevel", 1);
		GameObject.Find("MainLevelText").GetComponent<Text>().text = mainLevel.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GuidePanel.active)
			{
				GuidePanel.SetActive(false);
			}
			else
			{
				Application.Quit();
			}
		}
	}

	public void PlayAgainBtnClicked()
	{
		SceneManager.LoadScene(0);
	}

	public void AboutMeClicked()
	{
		Application.OpenURL("http://mohammadam.ir/");
	}

	public void GuideButtonClicked()
	{
		if (GuidePanel.active)
		{
			GuidePanel.SetActive(false);
		}
		else
		{
			GuidePanel.SetActive(true);
		}
	}
}
