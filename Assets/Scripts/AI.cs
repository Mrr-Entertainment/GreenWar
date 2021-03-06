
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	public int minSpwanInterval = 30;
	public int maxSpwanInterval = 60;
	Region[] regions;
	public GameObject enemyPrefab;

	void Awake()
	{
		GameEventManager.RegionConquered += RegionConquered;
		regions = FindObjectsOfType(typeof(Region)) as Region[];
	}

	void Start() {
		StartCoroutine(spawnEnemies());
	}

	void RegionConquered(Region region) {
		Debug.Log("RegionConquered");
		if (region.owner == Owner.Enemy) {
			StartCoroutine(attackNextRegion(region));
		}
	}
	private IEnumerator attackNextRegion(Region region)
	{
		int waitTime = Random.Range(0, 20);
		yield return new WaitForSeconds(waitTime);
		bool attack = false;
		foreach (var dest in region.neighbors) {
			if (dest.playerUnits.Count != 0) {
				attack = true;
				foreach(var unit in region.enemyUnits) {
					unit.moveToRegion(dest);
				}
			}
		}
		if (! attack) {
			foreach(var unit in region.enemyUnits) {
				int index = Random.Range(0, region.neighbors.Length) ;
				unit.moveToRegion(region.neighbors[index]);
			}
		}
	}

	private IEnumerator spawnEnemies() {
		var targetRegion1 = findSpawnPoint();
		var enemy1 = Instantiate(enemyPrefab, targetRegion1.transform.position, Quaternion.identity).GetComponent<Attacker>();
		StartCoroutine(attackNextRegion(targetRegion1));
		while(true) {
			int waitTime = Random.Range(minSpwanInterval, maxSpwanInterval);
			Debug.Log("Wait for enemy spawy" + waitTime);
			yield return new WaitForSeconds(waitTime);

			var targetRegion = findSpawnPoint();
			var enemy = Instantiate(enemyPrefab, targetRegion.transform.position, Quaternion.identity).GetComponent<Attacker>();
			StartCoroutine(attackNextRegion(targetRegion));
		}
	}

	Region findSpawnPoint() {
		List<Region> validRegions = new List<Region>();
		while(true) {
			//Empy regions
			foreach(var region in regions) {
				if (region.playerUnits.Count == 0 && region.enemyUnits.Count == 0) {
					validRegions.Add(region);
				}
			}
			if (validRegions.Count != 0) {
				break;
			}

			//Enemy regions
			foreach(var region in regions) {
				if (region.playerUnits.Count == 0) {
					validRegions.Add(region);
				}
			}
			if (validRegions.Count != 0) {
				break;
			}
			//Player regions
			foreach(var region in regions) {
				validRegions.Add(region);
			}
			if (validRegions.Count != 0) {
				break;
			}
			break;
		}
		int index = Random.Range(0, validRegions.Count - 1);
		return validRegions[index];
	}

}
