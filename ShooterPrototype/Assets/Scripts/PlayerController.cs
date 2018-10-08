using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

public class PlayerController : NetworkBehaviour {  

	// The Camera
	public Camera PlayerCamera;
	
	//Player movements
	public float Speed = 6.0f;
	public float rotateSpeed = 6.0f;
	public float jumpSpeed = 8.0f; 
	public float gravity = 20.0f;
	public float goDown;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private int jumps;


	public GameObject CrossHairCanvas; 


	// Color of the local player vs seing the other players as red
	public Color LocalPlayerColor;

	//Shooting
	public GameObject BulletPrefab;
	public Transform BulletSpawnPoint;
	public float BulletSpeed;
	public float ReloadRate = 0.5f;
	private float nextShotTime;


	//Particles for shooting
	public GameObject HitAffect;
	public ParticleSystem GunFlash;

	//Rotation
	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	// Crosshair for shooting
	public Texture Crosshair;

	//Pause the game
	[SerializeField]
	GameObject pauseMenu;

	//Spawnable objects/Barriers
	public Transform WallPoint;   
	public GameObject WallPrefab;


	//Reference to the player
	public GameObject ThePlayer;

	// Use this for initialization
	void Start () {

		controller = GetComponent <CharacterController> ();

		CrossHairCanvas = GameObject.Find ("CrossHairCanvas");

		PauseGame.IsOn = false;

	}

	/*void OnGUI()
	{
		float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (Crosshair.width / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (Crosshair.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, Crosshair.width, Crosshair.height), Crosshair);

	}*/


	public override void  OnStartLocalPlayer ()
	{
		var PlayerParts = GetComponentsInChildren<MeshRenderer> ();

		foreach (var parts in PlayerParts) 
		{
			parts.material.color = LocalPlayerColor;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//ThePlayer = GetComponent <NetworkManager> ().playerPrefab;

		// Determines which player computer it is on and makes only itself stuff controllable on this computer
		if (!isLocalPlayer)
		{
			//CrossHairCanvas.SetActive (false);
			PlayerCamera.enabled = false;
			return;

		}


		// Toggling pause menu
		if (Input.GetButtonDown ("StartButton")|| Input.GetKeyDown (KeyCode.Escape))
		{
			TogglePauseMenu ();
		}

		//Stops player from doing anything when the game is paused
		if (PauseGame.IsOn)
		return;
		
			//Shooting input
		if (Input.GetButtonDown ("R2Button") || Input .GetKeyDown (KeyCode.LeftShift) || Input.GetMouseButtonDown (0) && Time.time > nextShotTime)
		{
			GunFlash.Play ();
			CmdFire ();
		}

		//Building
		if (Input.GetButtonDown ("L1Button") && Time.time > nextShotTime)
		{
			CmdBuildWall ();
		}

			// if player is grounded move, jump, and rotate self
		if (controller.isGrounded)
		{

			//GoDown
			goDown = 0;
			//Player movement
			moveDirection =  new Vector3 (0, 0, Input.GetAxis ("Horizontal"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= Speed;
			if (Input.GetButtonDown ("XButton") || Input.GetKeyDown (KeyCode.Space))
			{
				moveDirection.y = jumpSpeed;
			}
			else 
			{
				//Player movement
				moveDirection =  new Vector3(0, moveDirection.y, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection.x *= Speed;
				moveDirection.z *= Speed;
			}
	
			//transform.Rotate (0, Input.GetAxis ("Mouse X"), 0);
			//transform.Rotate (0, Input.GetAxis ("Mouse Y"), 0);
		}



		if (!controller.isGrounded && Input.GetButtonDown ("XButton") || Input.GetKeyDown (KeyCode.Space))
		{
			goDown = 3;
			moveDirection.y -= jumpSpeed * goDown;
		}

		// Player rotation
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);

		//float xMin = (Screen.width - Input.mousePosition.x) - (Crosshair.width / 2);
		//float yMin = (Screen.height - Input.mousePosition.y) - (Crosshair.height / 2);



	}

	//spawning build objects
	void CmdBuildWall ()
	{
		GameObject TheWall = Instantiate (WallPrefab,WallPoint.position, WallPoint.rotation);
		NetworkServer.Spawn (TheWall);
	}

	// Spawning the bullet and shooting it forward fucntion
	[Command]
	void CmdFire ()
	{

		RaycastHit hit;
		if 	(Physics.Raycast (PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit))
		{
			Debug.Log (hit.transform.name);

			//damage anything with this script on it
			GenericHealth damage = hit.transform.GetComponent <GenericHealth> ();

			//Damage other players
			Health cause = hit.transform.GetComponent<Health> ();

			if (damage != null)
			{
				damage.Damaged ();
			}
				
			if (cause != null)
			{
				cause.TakeDamage (5);
			}

		}
			
		GameObject Bullet = Instantiate (HitAffect, hit.point, Quaternion.LookRotation (hit.normal));
		NetworkServer.Spawn (Bullet); 
		Destroy (Bullet, 2.0f);
	}

	// Taking damage
	/*void OnCollisionEnter (Collision collision)
	{
		var other = collision.gameObject;
		try {
			var causeDamageScript = other.GetComponent<CauseDamage> ();
			var totalDamage = causeDamageScript.GetDamage ();
			var HealthScript = GetComponent<Health> ();
			HealthScript.TakeDamage (totalDamage);

		} catch
		{
			Debug.Log ("Something hit but did not do damage");
		}
	}*/

	//  Togglw pause menu function
	void TogglePauseMenu ()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseGame.IsOn = pauseMenu.activeSelf;
	}
}
