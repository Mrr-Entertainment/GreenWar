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
	public int treeCount = 35000;
	public int maxTreeCount = 100000;
	public float pollution = 0.1f;
	public int population = 5000;
	public List<Attacker> enemyUnits;
	public List<Attacker> playerUnits;
	public float lastIncomeTime = 0;

	private PolygonCollider2D m_collider;

	void Start()
	{
		m_collider = GetComponent<PolygonCollider2D>();
	}

	void UpdatePollution()
	{
		float change = (float)(population/(treeCount*Math.Log10(population)));
		float ratio = treeCount/maxTreeCount;
		if(ratio >= 0.6f) {
			pollution -= change;
		} else {
			pollution += change;
		}
	}

	void Update() 
	{
		
	}

	public Bounds getBounds() {
		return m_collider.bounds;
	}
	public void enterRegion(Attacker attacker) {
		if (noUnitPresent()) {
			lastIncomeTime = Time.time;
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
	bool noUnitPresent() {
		return enemyUnits.Count == 0 && playerUnits.Count == 0;
	}

}
