
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	void Awake()
	{
		GameEventManager.RegionConquered += RegionConquered;
	}

	void RegionConquered(Region region) {
		Debug.Log("RegionConquered");
		if (region.owner == Owner.Enemy) {
			int index = Random.Range(0, region.neighbors.Length);
			Region dest = region.neighbors[index];
			Debug.Log("Now attack" + dest);
			//Great AI
			foreach(var unit in region.enemyUnits) {
				unit.targetRegion = dest;
			}
		}
	}
}
