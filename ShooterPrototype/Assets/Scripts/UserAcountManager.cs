﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAcountManager : MonoBehaviour {

	public static UserAcountManager instance;

	void Awake ()
	{
		if (instance != null)
		{
			Destroy (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (this);
	}

	public static string loggedIn_Username { get; protected set;} //stores username once logged in
	private static string loggedIn_Password = ""; //stores password once logged in

	public static bool IsLoggedIn { get; protected set;}

	public delegate void OnDataReceivedCallback (string data);


	public string loggedInScenename = "Lobby";

	public string loggedOutScenename = "Login";


	public void LogOut ()
	{
		loggedIn_Username = "";
		loggedIn_Password = "";

		IsLoggedIn = false;

		Debug.Log ("User Logged Out");

		SceneManager.LoadScene (loggedOutScenename);
	}

	public void LogIn (string username, string password)
	{
		loggedIn_Username = username;
		loggedIn_Password = password;

		IsLoggedIn = true;

		Debug.Log ("Logged in as " + username);

		SceneManager.LoadScene (loggedInScenename);

	}

	public void SendData (string data) { //called when the 'Send Data' button on the data part is pressed
		if (IsLoggedIn) {
			//ready to send request
			StartCoroutine (sendSendDataRequest (loggedIn_Username, loggedIn_Password, data)); //calls function to send: send data request

		}
	}

	IEnumerator sendSendDataRequest(string username, string password, string data) {


		IEnumerator eee = DCF.SetUserData (username, password, data);
		while (eee.MoveNext()) {
			yield return eee.Current;
		}
		WWW returneddd = eee.Current as WWW;
		if (returneddd.text == "ContainsUnsupportedSymbol") {
			//One of the parameters contained a - symbol
			Debug.Log ("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
		}
		if (returneddd.text == "Error") {
			//Error occurred. For more information of the error, DC.Login could
			//be used with the same username and password
			Debug.Log ("Data Upload Error: Contains Unsupported Symbol '-'");
		}
			
	}

	public void GetData (OnDataReceivedCallback onDataReceived) { //called when the 'Get Data' button on the data part is pressed

		if (IsLoggedIn) {
			//ready to send request
			StartCoroutine (sendGetDataRequest (loggedIn_Username, loggedIn_Password, onDataReceived)); //calls function to send get data request

		}
	}






	IEnumerator sendGetDataRequest(string username, string password, OnDataReceivedCallback onDataReceived) {

		string data = "ERROR";


		IEnumerator eeee = DCF.GetUserData (username, password);
		while (eeee.MoveNext()) {
			yield return eeee.Current;
		}
		//WWW returnedddd = eeee.Current as WWW;
		string response = eeee.Current as string; // << The returned string

		if (response == "Error") {
			//Error occurred. For more information of the error, DC.Login could
			//be used with the same username and password
			Debug.Log ("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
		} else {
			if (response == "ContainsUnsupportedSymbol") {
				//One of the parameters contained a - symbol
				Debug.Log ("Get Data Error: Contains Unsupported Symbol '-'");
			} else {
				//Data received in returned.text variable
				string DataRecieved = response;
				data = DataRecieved;
			}
		}

		if (onDataReceived != null)
			onDataReceived.Invoke (data); 
	}


}
