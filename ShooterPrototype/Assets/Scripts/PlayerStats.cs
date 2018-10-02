﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public Text killCount;
	public Text deathCount;


	// Use this for initialization
	void Start () {

		if (UserAcountManager.IsLoggedIn)
			UserAcountManager.instance.GetData (OnReceivedData);
	}

	void OnReceivedData (string data)
	{
		killCount.text = DataTranslator.DataToKills (data).ToString () + "KILLS";
		deathCount.text = DataTranslator.DataToDeaths (data).ToString () + "DEATHS";


	}

}
