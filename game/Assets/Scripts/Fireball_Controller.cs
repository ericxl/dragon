using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fireball_Controller : NetworkBehaviour {
	[SerializeField] float lifetime = 4f;
	[SerializeField] bool cankill = false;


	bool isLive = true;
	float age;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//[ServerCallback]
	void FixedUpdate () {
		
		age += Time.deltaTime;
		if (age > lifetime) {
			NetworkServer.Destroy (gameObject);
		} else {

			transform.Translate (Vector2.right * Time.deltaTime*8);
		}
	}
	[Command]
	void CmdMove() {
		
		transform.Translate (Vector2.right * Time.deltaTime*8);
	}
	void OnCollisionEnter(Collision other) {
		if (!isLive)
			return;
		isLive = false;


		if (!isServer)
			return;

		if (!cankill || other.gameObject.tag != "Player")
			return;

		Debug.Log ("you hit a player");
	}
}
