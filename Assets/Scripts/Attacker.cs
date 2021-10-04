using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
	public Owner owner;
	public int attackPower;
	public int incomePower;
	public float speed = 3.0f;
	public int health;
	public int maxHealth = 100;
	public Region  currentRegion;
	public Region  targetRegion;
	private Collider2D m_collider;
	private float m_startZ;
	private NavMeshAgent2D m_agent;
	public GameObject selectedIcon;
	public GameObject unselectedIcon;


	enum WanderState {
		Wander,
		Wait,
	}
	private WanderState m_wanderState = WanderState.Wander;



	void Start()
	{
		m_agent = GetComponent<NavMeshAgent2D>();
		m_startZ = transform.position.z;
		m_collider = GetComponent<Collider2D>();
		whereIAm();
	}

	// Update is called once per frame
	void Update()
	{
		var pos = transform.position;
		pos.z = m_startZ;
		transform.position = pos;
	}

	// called when this GameObject collides with GameObject2.
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Region" && currentRegion != targetRegion) {
			currentRegion.leaveRegion(this);
			currentRegion = col.gameObject.GetComponent<Region>();
			currentRegion.enterRegion(this);
		}
	}


	void setWanderPoint() {
		var bounds = currentRegion.getBounds();
		var center = bounds.center;

		int attempt = 0;
		Vector3 target = currentRegion.transform.position;
		Debug.Log("Wander in " + currentRegion);
		do {
			target.x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
			target.y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
			attempt++;
			Debug.Log("Try Wander point to " + target);
			if (m_collider.OverlapPoint(target)) {
				break;
			}
		} while (attempt <= 100);
		if (attempt > 100) {
			target = currentRegion.transform.position;
		}
		target.z = transform.position.z;
		Debug.Log("SEt Wander point to " + target);
		m_agent.destination = target;
	}

	public void moveToRegion(Region region){
		if (region == null ||  targetRegion == region) {
			return;
		}
		targetRegion = region;

		var pos  = targetRegion.transform.position;
		pos.z = m_startZ;
		m_agent.destination = pos;
	}


	void whereIAm(){
		ContactFilter2D filter = new ContactFilter2D();
		filter.useTriggers = true;
		Collider2D[] overlaps = new Collider2D[10];
		int count = m_collider.OverlapCollider(filter, overlaps);
		foreach(Collider2D obj in overlaps) {
			if (!obj) {
				continue;
			}
			if (obj.tag == "Region") {
				currentRegion = obj.gameObject.GetComponent<Region>();
				currentRegion.enterRegion(this);
				break;
			}
		}
	}

	public void selectUnit() {
		selectedIcon.SetActive(true);
		unselectedIcon.SetActive(false);
	}
	public void unselectUnit() {
		selectedIcon.SetActive(false);
		unselectedIcon.SetActive(true);
	}
}
