using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {

	public float lookRadius = 10.0f;

	Transform target; // the target the enemy needs to track which be used as a reference from singleton code

	NavMeshAgent agent; //Getting reference to the navmesh component attached to the Enemy

	public Animator anim; //Getting reference to the animator component attached to the Enemy

	public GameObject LeftHand;
	public GameObject RightHand;

	public enum EnemyState //Each animation state
	{
		Idle,
		Walk,
		Attack,
		Death
	}

	public EnemyState CurrentState; // referecnes 

	// Use this for initialization
	void Start () {

		target = PlayerTracker.instance.player.transform; //Reference to singleton code that tracks the player    
		agent = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		ChangeState (EnemyState.Idle); // start on idle and first example on how to switch states

	}


	
	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance (target.position, transform.position); // distance between the player and the enemy

		if (distance <= lookRadius) 
		{
			agent.SetDestination (target.position);
			anim.SetInteger ("States", 1);

			if (distance <= agent.stoppingDistance)
			{
				
				anim.SetInteger ("States", 2); //Attack the target
				LeftHand.SetActive (true); //Hit box when attacking player
				RightHand.SetActive (true); //Hit box when attacking player
				//Face target
				FaceTarget ();
			}
		}
	}

	void FaceTarget () // Face the target if in look radius
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected () //Shows the sight radius on the ai
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}

	public void ChangeState (EnemyState newState) // Function to control when switching states
	{
		CurrentState = newState;

	}


	//Chararcter States
	IEnumerator IdleState ()
	{
		while (CurrentState == EnemyState.Idle )
		{
			anim.SetInteger ("States", 0); // Instead of using bools and fucntions to set up the states in the code, I used IEnumerators and ints. The IEnumerator are being used for if i need to use the "WaitForSeconds to do this", it makes it easier and smoother instead of using a timer or something to leave to another stae after something that was supposed to happens time has passed already
			yield return null;
		}
	}

	IEnumerator WalkState ()
	{
		while (CurrentState == EnemyState.Idle )
		{
			anim.SetInteger ("States", 1);
			yield return null;
		}
	}

	IEnumerator AttackState ()
	{
		while (CurrentState == EnemyState.Idle )
		{
			anim.SetInteger ("States", 2);
			yield return null;
		}
	}

	IEnumerator DeathState ()
	{
		while (CurrentState == EnemyState.Idle )
		{
			anim.SetInteger ("States", 3);
			yield return null;
		}
	}
}
