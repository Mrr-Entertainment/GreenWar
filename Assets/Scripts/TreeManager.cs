using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeManager : MonoBehaviour
{
	public Region[] regions;
	public float incomeSpeed;

	void Start()
	{
		regions = FindObjectsOfType(typeof(Region)) as Region[];
	}

	void Update()
	{
		foreach(Region region in regions) {
			if (Time.time - region.lastIncomeTime < incomeSpeed) {
				continue;
			}
			region.lastIncomeTime = Time.time;
			// Only player
			if (region.enemyUnits.Count == 0 && region.playerUnits.Count != 0) {
				int sum = 0;
				foreach (var unit in region.playerUnits) {
					sum+= unit.incomePower;
				}

				if(region.treeCount > 0) {
					region.income = sum;
					region.addTrees(-(sum * 10));
					if(region.treeCount < 0){
						region.treeCount = 0;
					}
					//Show popup with funds
				}
				else {
					region.income = 0;
				}
			} else if (region.enemyUnits.Count != 0 && region.playerUnits.Count == 0) {
				//GenerateTrees
				int sum = 0;
				foreach (var unit in region.enemyUnits) {
					sum+= unit.incomePower;
				}
				region.addTrees(sum);
				region.income = 0;
			} else {
				region.income = 0;
			}
		}
	}
}
