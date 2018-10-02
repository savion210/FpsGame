using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsKisematic : MonoBehaviour {

	Rigidbody self;

	// Use this for initialization
	void Start () {
		Kisematic ();
		self = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Kisematic ()
	{
		yield return new WaitForSeconds (0.5f);
		self.isKinematic = false;
	}
}

