using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public int MaxHealth;




	[SyncVar(hook = "OnHealthChanged")]
	public int currentHealth;
	public Text HealthScore;

	// Use this for initialization
	void Start () {
		HealthScore.text = currentHealth.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage (int howmuch)
	{
		currentHealth -= howmuch;
			
		if (!isServer)
		{
			return;
		}

		var newHealth = currentHealth - howmuch;

		if (newHealth <= 0)
		{
			//Debug.Log ("Dead");
			currentHealth = MaxHealth;
			RpcDeath ();
		} else
		{
			currentHealth = newHealth;
		}
	}

	void OnHealthChanged (int updatedHealth)
	{
		HealthScore.text = updatedHealth.ToString ();

	}

	[ClientRpc]
	void RpcDeath ()
	{
		if (isLocalPlayer)
		{
			//transform.position = Vector3.zero;
			var spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
			var chosenPoint = Random.Range (0, spawnPoints.Length);
			transform.position = spawnPoints [chosenPoint].transform.position;
		}
	}


}
