﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataTranslator : MonoBehaviour {

	public static string KILLS_SYMBOL = "[KILLS]"; 

	public static string DEATH_SYMBOL = "[DEATHS]"; 


	public static int DataToKills (string data)
	{
		return int.Parse (DataToValue (data, KILLS_SYMBOL));
	}

	public static int DataToDeaths (string data)
	{
		return int.Parse (DataToValue (data, DEATH_SYMBOL));

	}

	private static string DataToValue (string data, string symbol)
	{
		string[] pieces = data.Split ('/');

		foreach (string piece in pieces)
		{
			if (piece.StartsWith (symbol)) 
			{
				return piece.Substring (symbol.Length);
			}
		}
		Debug.LogError (symbol + "not found in" + data);  
		return "";
	}
}
