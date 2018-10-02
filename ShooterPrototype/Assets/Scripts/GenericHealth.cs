using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour {

	public float Health = 20;

	public float damageDealt = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DestroySelf (); 
	}

	public void Damaged ()
	{
		Health -= damageDealt;
	}

	void DestroySelf ()
	{
		if (Health <= 1)
		{
			Destroy (gameObject);
		}
	}
}
