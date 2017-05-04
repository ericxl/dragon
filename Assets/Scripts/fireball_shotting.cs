using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class fireball_shotting : NetworkBehaviour{

	[SerializeField] GameObject firball;
	[SerializeField] Transform hand;

	Animator anim;

	// Use this for initialization
	void Reset () {
		hand = transform.FindChild ("hand");
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) {
			// x = CrossPlatformInputManager.GetAxis ("Horizontal");
			//float y = CrossPlatformInputManager.GetAxis("Vertical");
			float x = Input.GetAxis ("Horizontal");
			float y = Input.GetAxis("Vertical");
			float angle = Mathf.Atan2 (y, x)*Mathf.Rad2Deg;
			angle = (angle == 0) ? hand.rotation.z : angle; 
			CmdSpawnFireBall (angle);
		}
	}

	[Command]
	void CmdSpawnFireBall(float angle) {
		hand.rotation = Quaternion.Euler(0,0,angle);
		GameObject instance = Instantiate (firball, hand.position, hand.rotation) as GameObject;
		instance.GetComponent<Rigidbody2D> ().AddForce (hand.forward * 1000);


		NetworkServer.Spawn (instance);

	}
}
