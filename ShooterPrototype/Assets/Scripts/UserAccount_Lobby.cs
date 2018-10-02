using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAccount_Lobby : MonoBehaviour {

	public Text usernameText;

	void Start ()
	{
		if (UserAcountManager.IsLoggedIn)
			usernameText.text = UserAcountManager.loggedIn_Username;
	}

	public void LogOut ()
	{
		if (UserAcountManager.IsLoggedIn)
			UserAcountManager.instance.LogOut ();
	}
}
