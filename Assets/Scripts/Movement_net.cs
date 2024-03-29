using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
public class Movement_net : NetworkBehaviour{
	Rigidbody2D rbody;
	Animator anim;
	Transform mycam;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			Destroy (this);
			return;
		}

		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		mycam = Camera.main.transform;
		movecam ();
	}
	
	// Update is called once per frame Fixed
	void FixedUpdate () {
		
		//Vector2 movement_vector = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 movement_vector = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
		if (movement_vector != Vector2.zero) {
			anim.SetBool ("is_walking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		} else {
			anim.SetBool ("is_walking", false);
		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime * 5);

		//Cmdmove (movement_vector);
		movecam ();
	}
		
	void movecam() {
		//mycam.orthographicSize = (Screen.height / 100f) / 1f;

		if (rbody) {

			mycam.position = Vector3.Lerp (transform.position, rbody.position, 0.1f) + new Vector3(0, 0, -10);
			//transform.position = Vector3.Lerp (transform.position, rbody.position, 0.1f) + new Vector3(0, 0, -10);
		}


	}




}
