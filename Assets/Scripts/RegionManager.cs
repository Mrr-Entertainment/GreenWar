using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RegionManager : MonoBehaviour
{
	private CircleCollider2D m_collider;


	void setNeighbor(){
		Region[] list = FindObjectsOfType(typeof(Region)) as Region[];
		foreach(Region region in list) {

			HashSet<Region> neighbors  = new HashSet<Region>();
			PolygonCollider2D collider = region.GetComponent<PolygonCollider2D>();

			foreach(Vector2 point in collider.points) {
				var overlaps = Physics2D.OverlapCircleAll(collider.transform.TransformPoint( point), 1f);
				foreach(Collider2D obj in overlaps) {
					if (!obj) {
						continue;
					}
					var t = obj.GetComponent<Region>();
					if (obj.tag == "Region" ) {
						var tempReg = obj.GetComponent<Region>();
						if (tempReg != region) {
							neighbors.Add(tempReg);
						}
					}
				}
			}
			region.neighbors = neighbors.ToArray();
		}

	}

	void Start()
	{
		m_collider = GetComponent<CircleCollider2D>();
		setNeighbor();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
