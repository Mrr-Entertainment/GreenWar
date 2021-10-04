using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public Region[] regions;
	public Region mainRegion;
	public GameObject unitPrefab;
	public Attacker selectedUnit;
	void Start()
	{
		regions = FindObjectsOfType(typeof(Region)) as Region[];
	}

	void Update()
	{
		foreach(Region region in regions) {
			resolveBattle(region);
		}

		if (Input.GetMouseButton(0)) {
			OnClicked();
		}
	}

	void resolveBattle(Region region) {
		if (region.enemyUnits.Count != 0 && region.playerUnits.Count != 0) {
			attachEachOther(region);
		}
	}
	void attachEachOther(Region region) {
		//TODO: retreat the enemy of hp is to low
		attack(region.enemyUnits, region.playerUnits, region);
		attack(region.playerUnits, region.enemyUnits, region);
	}

	void attack (List<Attacker> attackers, List<Attacker> defenders, Region region) {
		foreach (var unit in attackers ) {
			int index = Random.Range(0, defenders.Count -1);
			defenders[index].health -= unit.attackPower;
			if (! isAlive(defenders[index])) {
				Debug.Log("Unit killed");
				Destroy(defenders[index].gameObject, 0.5f);
				defenders.RemoveAt(index);
				if (defenders.Count == 0) {
					GameEventManager.TriggerRegionConquered(region);
				}
			}
		}
	}

	bool isAlive(Attacker unit) {
		return unit.health > 0;
	}

	public void spawnPlayerUnit() {
		Instantiate(unitPrefab, mainRegion.transform.position, Quaternion.identity);
	}

	void OnClicked()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		bool unitFound = false;
		Region tempRegion = null;
		foreach(var hit in hits) {
			if(hit.rigidbody != null){
				Attacker unit = hit.rigidbody.gameObject.GetComponent<Attacker>();
				if (unit != null && unit.owner == Owner.Player) {
					selectedUnit = unit;
					unitFound = true;
					break;
				}
			} else if (hit.collider != null) {
				Region region = hit.collider.gameObject.GetComponent<Region>();
				if (region != null) {
					tempRegion = region;
				}
			}
		}
		if (! unitFound && tempRegion != null) {
			if (selectedUnit != null) {
				selectedUnit.moveToRegion(tempRegion);
			}

		}
	}
}
