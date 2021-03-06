using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PollutionManager : MonoBehaviour
{
    Region[] regions;
    float lastUpdate;
    float globalPollution;
    public TextMeshProUGUI pollutionText;
	public GameObject endScreen;

    bool firstUpdate = true;

    void Start()
    {
        regions = FindObjectsOfType(typeof(Region)) as Region[]; 
    }


    void Update()
    {
        if((Time.time - lastUpdate < 10f) && !firstUpdate){
            firstUpdate = false;
            return;
        }

        float pollution = 0f;
        foreach(Region region in regions){    
            pollution += region.pollution;
        }

        globalPollution = pollution / regions.Length;
        pollutionText.text = (int)(globalPollution*100) + "%";
        lastUpdate = Time.time;

		if (globalPollution >= 1) {
			Time.timeScale = 0;
			endScreen.SetActive(true);
		}
    }
}
