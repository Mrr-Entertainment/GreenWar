using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
	//Find a way to populate at runtime
	public Region[] neighbors;
	public Owner owner = Owner.Neutral;
	public int score = 100;
	public float treeCount = 3500;
	public float maxTreeCount = 20000;
	public float pollution = 0.1f;
	public int population = 5000;
	public List<Attacker> enemyUnits;
	public List<Attacker> playerUnits;
	public float lastIncomeTime = 0;

	float lastUpdate;

	public GameObject[] forestLevel;
	public int currentForestLevelIndex;

	private PolygonCollider2D m_collider;

	public int income { get; set; }

	void Start()
	{
		lastUpdate = Time.time;
		m_collider = GetComponent<PolygonCollider2D>();
		foreach (var tree in forestLevel) {
			tree.SetActive(false);
		}
		UpdateForestLevel();
		income = 0;
		UpdatePollution();
	}

	void UpdatePollution()
	{
		/*float change = (float)(population/(treeCount*Math.Log10(population)));
		float ratio = treeCount/maxTreeCount;
		if(ratio >= 0.6f) {
			pollution -= change;
		} else {
			pollution += change;
		}*/
		if(treeCount == 0)
		{
			pollution = 1f;
			return;
		}
		pollution = 1 - treeCount/maxTreeCount;
	}

	public void UpdateForestLevel()
	{
		float forestness = treeCount/maxTreeCount;
		int forestLevelIndex = 0;
		if (forestness < 0.2){
			forestLevelIndex = 0;
		} else if(forestness >= 0.2 && forestness < 0.4){
			forestLevelIndex = 1;
		} else if(forestness >= 0.4 && forestness < 0.6){
			forestLevelIndex = 2;
		} else if(forestness >= 0.6 && forestness < 0.8){
			forestLevelIndex = 3;
		} else if(forestness >= 0.8){
			forestLevelIndex = 4;
		}

		if(forestness < 0.01f){
			forestLevel[0].SetActive(false);
		} else {
			forestLevel[0].SetActive(true);
		}

		if (forestLevelIndex > forestLevel.Length - 1) {return;}
		if(forestLevelIndex != currentForestLevelIndex){
			forestLevel[currentForestLevelIndex].SetActive(false);
			forestLevel[forestLevelIndex].SetActive(true);
			currentForestLevelIndex = forestLevelIndex;
		}
	}

	void Update()
	{
		if(Time.time - lastUpdate < 10f) {
            return;
        }
		lastUpdate = Time.time;
		UpdatePollution();
	}

	public Bounds getBounds() {
		return m_collider.bounds;
	}
	public void enterRegion(Attacker attacker) {
		if (noUnitPresent()) {
			lastIncomeTime = Time.time;
			GameEventManager.RegionConquered(this);
		}
		switch(attacker.owner) {
			case Owner.Enemy:
				{
					enemyUnits.Add(attacker);
					break;
				}
			case Owner.Player:
				{
					playerUnits.Add(attacker);
					break;
				}
		}
	}

	public void leaveRegion(Attacker attacker) {

		switch(attacker.owner) {
			case Owner.Enemy:
				{
					enemyUnits.Remove(attacker);
					if (enemyUnits.Count == 0) {
						lastIncomeTime = Time.time;
					}
					break;
				}
			case Owner.Player:
				{
					playerUnits.Remove(attacker);
					if (playerUnits.Count == 0) {
						lastIncomeTime = Time.time;
					}
					break;
				}
		}
	}

	public void addTrees(int treeSum ) {
		treeCount = Mathf.Clamp(treeCount + treeSum, 0, maxTreeCount);
		UpdateForestLevel();
		
	}
	bool noUnitPresent() {
		return enemyUnits.Count == 0 && playerUnits.Count == 0;
	}

}
