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
	public Region  currentRegion;
	public Region  targetRegion;
	private Collider2D m_collider;


	private Vector3 m_wanderDestination;
	enum WanderState {
		Wander,
		Wait,
	}
	private WanderState m_wanderState = WanderState.Wander;



	void Start()
	{
        m_collider = GetComponent<Collider2D>();
		whereIAm();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		movement();
	}

	// called when this GameObject collides with GameObject2.
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("GameObject1 collided with " + col.name);
		if (col.tag == "Region" && currentRegion != targetRegion) {
			currentRegion.leaveRegion(this);
			currentRegion = col.gameObject.GetComponent<Region>();
			currentRegion.enterRegion(this);
		}
	}
	void movement() {

		if (currentRegion != targetRegion) {
			moveToTargetRegion();
		}
		//wander
		if (currentRegion == targetRegion) {
			wander();
			if (currentRegion.owner != Owner.Enemy)  {
				currentRegion.score -= attackPower;
			}
		}
	}


	void wander() {
		if (Vector2.Distance(m_wanderDestination,  transform.position) > 0.1 && m_wanderState == WanderState.Wander) {

			float step =  speed * Time.fixedDeltaTime; // calculate distance to move
			var pos = Vector3.MoveTowards(transform.position, m_wanderDestination, step);
			pos.z = transform.position.z;
			transform.position = pos;
		} else {
			if (WanderState.Wander == m_wanderState) {
				m_wanderState = WanderState.Wait;
				StartCoroutine(WaitToWander());
			}
		}
	}

	IEnumerator WaitToWander()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(1,5));
		setWanderPoint();
		m_wanderState = WanderState.Wander;
	}

	void setWanderPoint() {
		var bounds = currentRegion.getBounds();
		var center = bounds.center;
		/* Debug.Log("Start to wander to " + center); */

		int attempt = 0;
		Vector3 target = m_wanderDestination;
		do {
			target.x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
			target.y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
			attempt++;
		} while (!GetComponent<Collider2D>().OverlapPoint(target) || attempt <= 100);
		target.z = transform.position.z;
		/* Debug.Log("Wander to  " + target); */
		m_wanderDestination = target;
	}

	void moveToTargetRegion(){
		if (!targetRegion) {
			return;
		}
		float step =  speed * Time.fixedDeltaTime; // calculate distance to move

		var pos  = Vector3.MoveTowards(transform.position, targetRegion.transform.position, step);
		pos.z = transform.position.z;
		transform.position = pos;

		m_wanderDestination = targetRegion.transform.position;
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
}
