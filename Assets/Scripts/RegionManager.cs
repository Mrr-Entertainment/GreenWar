using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* public class Region { */
/* 	public int id; */
/* 	public GameObject gameObject; */
/* } */

public class RegionManager : MonoBehaviour
{
	private CircleCollider2D m_collider;


	void setNeighbor(){
        Region[] list = FindObjectsOfType(typeof(Region)) as Region[];
		ContactFilter2D filter = new ContactFilter2D();
		filter.useTriggers = true;
		foreach(Region region in list) {             

			HashSet<GameObject> neighbors  = new HashSet<GameObject>();
			PolygonCollider2D collider = region.GetComponent<PolygonCollider2D>();
			foreach(Vector2 point in collider.points) {
				transform.position = point;
				Collider2D[] overlaps = new Collider2D[list.Length];
				int count = m_collider.OverlapCollider(filter, overlaps);
				foreach(Collider2D obj in overlaps) {
					if (!obj) {
						continue;
					}
					if (obj.tag == "Region") {
						neighbors.Add(obj.gameObject);
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
