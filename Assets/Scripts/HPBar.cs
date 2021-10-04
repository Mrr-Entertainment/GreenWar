using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	public Attacker attacker;
	public SpriteRenderer green;
	public SpriteRenderer red;

    // Start is called before the first frame update
    void Start()
    {
        attacker = GetComponent<Attacker>();
    }

    // Update is called once per frame
    void Update()
    {
		float ratio = attacker.health / attacker.maxHealth;

    }
}
