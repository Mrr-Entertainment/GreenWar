using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    float happiness;
    float water;
    float food;
    float services;
    float recreation;
    public int funds { get; set; }

    float lastUpdate;


    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI servicesText;
    public TextMeshProUGUI recreationText;

    void Start()
    {
        water = 0.5f;
        food = 0.35f;
        services = 0.25f;
        recreation = 0.1f;
        happiness = (0.35f*water + 0.30f*food + 0.20f*services + 0.15f*recreation)/4;
        
        lastUpdate = Time.time;
    }

    void Update()
    {
        happinessText.text = "Happiness: " + (int)(happiness*100) + "%"; 
        waterText.text = "Water: " + (int)(water*100) + "%";
        foodText.text = "Food: " + (int)(food*100) + "%";
        servicesText.text = "Services: " + (int)(services*100) + "%";
        recreationText.text = "Recreation: " + (int)(recreation*100) + "%";
        if(Time.time - lastUpdate < 10f) {
            return;
        }

        happiness = (0.35f*water + 0.30f*food + 0.20f*services + 0.15f*recreation)/4;
        lastUpdate = Time.time;
    }

    public void AddWater()
    {
        water += 0.25f;
        if(water > 1f){
            water = 1f;
        }
    }

    public void AddFood()
    {
        food += 0.20f;
        if(food > 1f){
            food = 1f;
        }
    }

    public void AddServices()
    {
        services += 0.15f;
        if(services > 1f){
            services = 1f;
        }
    }

    public void AddRecreation()
    {
        recreation += 0.10f;
        if(recreation > 1f){
            recreation = 1f;
        }
    }
}
