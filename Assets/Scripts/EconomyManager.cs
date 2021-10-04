using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    Region[] regions;

    float happiness;
    public float water { get; set; }
    public float food { get; set; }
    public float services { get; set; }
    public float recreation { get; set; }
    public int funds { get; set; }
    public int income { get; set; }
    public int basicIncome;
    int expenses;
    float lastUpdate;

	public TextMeshProUGUI fundsText;
    public TextMeshProUGUI incomeText;
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
        happiness = 0.35f*water + 0.30f*food + 0.20f*services + 0.15f*recreation;
        basicIncome = 500;
        income = basicIncome;
        expenses = 0;
        regions = FindObjectsOfType(typeof(Region)) as Region[];
        lastUpdate = Time.time;
    }

    void CalculateIncome()
    {
        income = basicIncome;
        foreach(Region region in regions){
            income += region.income;
        }
        income -= expenses;
    }

    void Update()
    {
        CalculateIncome();
        happinessText.text = "Happiness: " + (int)(happiness*100) + "%"; 
        waterText.text = "Water: " + (int)(water*100) + "%";
        foodText.text = "Food: " + (int)(food*100) + "%";
        servicesText.text = "Services: " + (int)(services*100) + "%";
        recreationText.text = "Recreation: " + (int)(recreation*100) + "%";
        incomeText.text = "Income: " + income + "$/h";
        fundsText.text = "Funds: " + funds + "$";
        if(Time.time - lastUpdate < 10f) {
            return;
        }
        funds += income;
        happiness = 0.35f*water + 0.30f*food + 0.20f*services + 0.15f*recreation;
        lastUpdate = Time.time;
    }

    public void PlusWater(int price)
    {
        if(water < 0.99f) {
            water += 0.25f;
            expenses += price;
            if(water > 1f){
                water = 1f;
            }
        }
    }
    public void MinusWater(int price)
    {   
        if(water > 0.01f) {
            water -= 0.25f;
            expenses += price;
            if(water < 0f){
                water = 0f;
            }
        }
    }

    public void PlusFood(int price)
    {
        if(food < 0.99f) {
            food += 0.20f;
            expenses += price;
            if(food > 1f){
                food = 1f;
            }
        }
    }
    public void MinusFood(int price)
    {
        if(food > 0.01f) {
            food -= 0.20f;
            expenses += price;
            if(food < 0f){
                food = 0f;
            }
        }
    }

    public void PlusServices(int price)
    {
        if(services < 0.99f) {
            services += 0.15f;
            expenses += price;
            if(services > 1f){
                services = 1f;
            }
        }
    }
    public void MinusServices(int price)
    {
        if(services > 0.01f) {
            services -= 0.15f;
            expenses += price;
            if(services < 0f){
                services = 0f;
            }
        }    
    }

    public void PlusRecreation(int price)
    {
        if(recreation < 0.99f) {
            recreation += 0.10f;
            expenses += price;
            if(recreation > 1f){
                recreation = 1f;
            }
        }
    }
    public void MinusRecreation(int price)
    {
        if(recreation > 0.01f) {
            recreation -= 0.10f;
            expenses += price;
            if(recreation < 0f){
                recreation = 0f;
            }
        }
    }
}
