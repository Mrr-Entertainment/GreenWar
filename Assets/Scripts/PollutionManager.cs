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

    void Start()
    {
        regions = FindObjectsOfType(typeof(Region)) as Region[];
        lastUpdate = Time.time;  
    }


    void Update()
    {
        if(Time.time - lastUpdate < 10f){
            return;
        }
        
        float pollution = 0f;
        foreach(Region region in regions){    
            pollution += region.pollution;
        }

        globalPollution = pollution / regions.Length;
        pollutionText.text = "Pollution: " + (int)(globalPollution*100) + "%";
        lastUpdate = Time.time;
    }
}
