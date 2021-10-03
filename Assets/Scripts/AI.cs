
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
			foreach (var dest in region.neighbors) {
				if (dest.owner == Owner.Enemy) {
					continue;
				}
				Debug.Log("Now attack" + dest);
				//Great AI
				foreach(var unit in region.enemyUnits) {
					unit.moveToRegion(dest);
				}

			}
		}
	}
}
