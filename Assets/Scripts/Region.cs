using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
	//Find a way to populate at runtime
	public GameObject[] neighbors;
	private PolygonCollider2D m_collider;
	public int owner;

    void Start()
    {
        m_collider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        
    }
	public Bounds getBounds() {
		return m_collider.bounds;
	}
}
