using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CossHairColorChange : MonoBehaviour {

	public GameObject CrossHair;

	public bool isHit;
	// Use this for initialization
	void Start () {

		CrossHair.GetComponent<Image>().color = new Color32(255,255,255,255);
		
	}
	
	// Update is called once per frame
	void Update () {

		
		RaycastHit hit;
		Ray Shoot = new Ray (transform.position, Vector3.forward);


		if (Physics.Raycast (Shoot, out hit))
		{
			if (hit.collider.tag == "Player")
			{
				Debug.Log ("Hit");
				CrossHair.GetComponent<Image>().color = new Color32(255,0,37,255);
			} 

		}


			



		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			CrossHair.GetComponent<Image>().color = new Color32(255,0,37,255);
		}
	}

	void OnTriggerStay (Collider others)
	{
		if (others.gameObject.tag == "Enemy")
		{
			CrossHair.GetComponent<Image>().color = new Color32(255,0,37,255);

		}
	}

	void OnTriggerExit (Collider others)
	{
		if (others.gameObject.tag == "Enemy")
		{
			CrossHair.GetComponent<Image>().color = new Color32(255,255,225,100);

		}
	}
}
