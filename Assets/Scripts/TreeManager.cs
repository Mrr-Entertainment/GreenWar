using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeManager : MonoBehaviour
{
	public Region[] regions;
	public int income = 0;
	public TextMeshProUGUI incomeText;

	void Start()
	{
		regions = FindObjectsOfType(typeof(Region)) as Region[];
		incomeText.text = income + "$";
	}

	void Update()
	{
		foreach(Region region in regions) {
			if (Time.time - region.lastIncomeTime < 2f) {
				continue;
			}
			region.lastIncomeTime = Time.time;
			// Only player
			if (region.enemyUnits.Count == 0 && region.playerUnits.Count != 0) {
				int sum = 0;
				foreach (var unit in region.playerUnits) {
					sum+= unit.incomePower;
				}
				income += sum;
				incomeText.text = income + "$";
				region.treeCount -= sum;
				//Show popup with money income

			} else if (region.enemyUnits.Count != 0 && region.playerUnits.Count == 0) {
				//GenerateTrees
				int sum = 0;
				foreach (var unit in region.playerUnits) {
					sum+= unit.incomePower;
				}
				region.treeCount += sum;
			}
		}
	}
}
