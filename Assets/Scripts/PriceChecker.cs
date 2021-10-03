using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceChecker : MonoBehaviour
{
    public enum Resource {
        Unit,
        Water,
        Food,
        Services,
        Recreation
    };

    EconomyManager economyManager;
    BattleManager battleManager;

    public int price = 100;
    public Resource type;
    

    void Start()
    {
        economyManager = GameObject.FindGameObjectsWithTag("Managers")[0].GetComponent<EconomyManager>();
        battleManager = GameObject.FindGameObjectsWithTag("Managers")[0].GetComponent<BattleManager>();
    }

    void Update()
    {
        if (price > economyManager.funds) {
            gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
        } else {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }    
    }

    public void OnButton()
    {
        if (price <= economyManager.funds) {
            switch(type){
                case Resource.Unit:
                    battleManager.spawnPlayerUnit();
                    break;
                case Resource.Water:       
                    economyManager.AddWater();
                    break;
                case Resource.Food:
                    economyManager.AddFood();
                    break;
                case Resource.Services:
                    economyManager.AddServices();
                    break;
                case Resource.Recreation:
                    economyManager.AddRecreation();
                    break;
            }
        }
    }
}
