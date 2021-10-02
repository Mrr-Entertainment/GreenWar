using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public Region[] regions;
    void Start()
    {
         regions = FindObjectsOfType(typeof(Region)) as Region[];
    }

    void FixedUpdate()
    {
		foreach(Region region in regions) {
			resolveBattle(region);
		}
    }

	void resolveBattle(Region region) {
		if (region.enemyUnits.Count != 0 && region.playerUnits.Count != 0) {
			attachEachOther(region);
		} else if  (region.enemyUnits.Count != 0 && region.owner != Owner.Enemy) {
			attackRegion(region.enemyUnits, region);
		} else if ((region.playerUnits.Count != 0 && region.owner != Owner.Player)) {
			attackRegion(region.playerUnits, region);
		}
	}
	void attachEachOther(Region region) {
		Debug.Log("attachEachOther");
		attack(region.enemyUnits, region.playerUnits);
		attack(region.playerUnits, region.enemyUnits);
	}

	void attack (List<Attacker> attackers, List<Attacker> defenders) {
		foreach (var unit in attackers ) {
			int index = Random.Range(0, defenders.Count -1);
			defenders[index].health -= unit.attackPower;
			if (! isAlive(defenders[index])) {
				Debug.Log("Unit killed");
				Destroy(defenders[index].gameObject, 1f);
				defenders.RemoveAt(index);
			}
		}
	}

	bool isAlive(Attacker unit) {
		return unit.health > 0;
	}
	void attackRegion(List<Attacker> attackers, Region region) {
		foreach (var unit in attackers ) {
			region.score -= unit.attackPower;
		}
		if (region.score <= 0) {
			region.score = 100;
			region.owner = attackers[0].owner;
		}

	}
}
