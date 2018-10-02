using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	List <GameObject> roomList = new List<GameObject> ();

	private NetworkManager networkManager;

	[SerializeField]
	private  Text status;

	[SerializeField]
	private GameObject roomListItemPrefab; 


	[SerializeField]
	private Transform roomListParent;

	// Use this for initialization
	void Start () {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
		{
			networkManager.StartMatchMaker ();
		}

		RefreshRoomList ();
	}

	public void RefreshRoomList ()
	{
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0, 8, "",true, 0, 0, OnMatchList); 
		status.text = "Loading...";
	}

	public void OnMatchList (bool success, string extendedinfo, List <MatchInfoSnapshot>matchlist) 
	{
		status.text = "";

		if (!success || matchlist == null) 
		{
			status.text = "Could not get matches";
			return;
		}
			
		foreach (MatchInfoSnapshot match in matchlist)
		{
			GameObject _roomListItemGo = Instantiate (roomListItemPrefab);
			_roomListItemGo.transform.SetParent (roomListParent);

			RoomListItem _roomListItem = _roomListItemGo.GetComponent <RoomListItem> ();

			if (_roomListItem != null)
			{
				_roomListItem.Setup (match, JoinRoom);
			}


			roomList.Add (_roomListItemGo);
		}

		if (roomList.Count == 0)
		{
			status.text = "No rooms at the moment";
		}

	}

	void ClearRoomList ()
	{
		for (int i = 0; i < roomList.Count; i++)   
		{
			Destroy (roomList[i]);   
		}

		roomList.Clear ();
	}


	public void JoinRoom (MatchInfoSnapshot _match)
	{
		networkManager.matchMaker.JoinMatch (_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		ClearRoomList ();
		status.text = "Joining...";
	}
	// Update is called once per frame
	void Update () {
		
	}
}
