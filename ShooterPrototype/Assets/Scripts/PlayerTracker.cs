using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTracker : NetworkBehaviour {

	#region Singleton


	public static PlayerTracker instance;

	void Awake ()
	{
		if (!isLocalPlayer)
		{
			instance = this;
		}

	}

	#endregion

	public GameObject player;

}
