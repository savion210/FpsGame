using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooting : MonoBehaviour {

	public Camera FpsCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Shoot ();
	}

	void Shoot ()
	{
		RaycastHit hit;
		if 	(Physics.Raycast (FpsCam.transform.position, FpsCam.transform.forward, out hit))
		{
			
		}

	}
}
