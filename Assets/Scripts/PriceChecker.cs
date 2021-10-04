using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceChecker : MonoBehaviour
{
    public enum Resource {
        Unit,
        WaterPlus,
        WaterMinus,
        FoodPlus,
        FoodMinus,
        ServicesPlus,
        ServicesMinus,
        RecreationPlus,
        RecreationMinus
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
        if(type == Resource.Unit)
        {
            if (price > economyManager.funds) {
                gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
            } else {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        /*} else if( type == Resource.WaterPlus      || 
                   type == Resource.FoodPlus       ||
                   type == Resource.ServicesPlus   ||   
                   type == Resource.RecreationPlus ) {
            if(price > economyManager.income){
                gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
            } else {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }*/
        } else {
            switch(type) {
                case Resource.WaterMinus:
                    if(economyManager.water < 0.01f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.FoodMinus:
                    if(economyManager.food < 0.01f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.ServicesMinus:
                    if(economyManager.services < 0.01f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.RecreationMinus:
                    if(economyManager.recreation < 0.01f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.WaterPlus:
                    if(economyManager.water > 0.99f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.FoodPlus:
                    if(economyManager.food > 0.99f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.ServicesPlus:
                    if(economyManager.services > 0.99f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
                case Resource.RecreationPlus:
                    if(economyManager.recreation > 0.99f) {
                        gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    } else {
                        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);        
                    }
                    break;
            }
        }
    }

    public void OnButtonUnit()
    {
        if (price <= economyManager.funds) {
            economyManager.funds -= price;
            battleManager.spawnPlayerUnit();
        }
    }

    public void OnButtonService()
    {
        int cost = price;
      //  if (price <= economyManager.income) {
            
            switch(type){
                case Resource.WaterPlus:       
                    economyManager.PlusWater(cost);
                    break;
                case Resource.WaterMinus:       
                    economyManager.MinusWater(cost);
                    cost *= -1;
                    break;
                case Resource.FoodPlus:
                    economyManager.PlusFood(cost);
                    break;
                case Resource.FoodMinus:
                    economyManager.MinusFood(cost);
                    cost *= -1;
                    break;
                case Resource.ServicesPlus:
                    economyManager.PlusServices(cost);
                    break;
                case Resource.ServicesMinus:
                    economyManager.MinusServices(cost);
                    cost *= -1;
                    break;
                case Resource.RecreationPlus:
                    economyManager.PlusRecreation(cost);
                    break;
                case Resource.RecreationMinus:
                    economyManager.MinusRecreation(cost);
                    cost *= -1;
                    break;
            }
            economyManager.income += cost;
       // }
    }
}
