using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hosting : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 8;

	private string roomName;

	private NetworkManager netwrokManager;

	void Start ()
	{
		netwrokManager = NetworkManager.singleton;
		if (netwrokManager.matchMaker == null)
		{
			netwrokManager.StartMatchMaker ();
		}
	}

	public void SetRoomName (string _name)
	{
		roomName = _name;
	}

	public void CreateRoom ()
	{
		if (roomName != "" && roomName != null)
		{
			Debug.Log ("Creating Room:" + roomName + "with room for" + roomSize + "players");

			// Create room
			netwrokManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, netwrokManager.OnMatchCreate);

		}

	}
}
