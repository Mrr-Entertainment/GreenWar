using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Region  currentRegion;
	public Region  targetRegion;

	public float speed = 3.0f;
	private Vector3 m_wanderDestination;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (currentRegion != targetRegion) {
			float step =  speed * Time.deltaTime; // calculate distance to move

			var pos  = Vector3.MoveTowards(transform.position, targetRegion.transform.position, step);
			pos.z = transform.position.z;
			transform.position = pos;

			m_wanderDestination = targetRegion.transform.position;
		}
		//wander
		if (currentRegion == targetRegion) {
			if (Vector2.Distance(m_wanderDestination,  transform.position) > 0.1) {

				float step =  speed * Time.deltaTime; // calculate distance to move
				var pos = Vector3.MoveTowards(transform.position, m_wanderDestination, step);
				pos.z = transform.position.z;
				transform.position = pos;
			} else {
				setWanderPoint();
			}
		}

	}
	// called when this GameObject collides with GameObject2.
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("GameObject1 collided with " + col.name);
		if (col.tag == "Region") {
			currentRegion = col.gameObject.GetComponent<Region>();
		}
	}


	void setWanderPoint() {
		var bounds = currentRegion.getBounds();
        var center = bounds.center;
		Debug.Log("Start to wander to " + center);

		int attempt = 0;
		Vector3 target = m_wanderDestination;
		do {
			target.x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
			target.y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
			attempt++;
		} while (!GetComponent<Collider2D>().OverlapPoint(target) || attempt <= 100);
		target.z = transform.position.z;
		Debug.Log("Wander to  " + target);
		m_wanderDestination = target;
	}
}
