using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public GameObject tutorialScreen;
	// Start is called before the first frame update
	void Start()
	{
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void hideTutorial() {
		tutorialScreen.SetActive(false);
		Time.timeScale = 1;
	}

}
