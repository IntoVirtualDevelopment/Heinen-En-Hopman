using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestMovement : NetworkBehaviour {
	private void Start() {
		if(!isLocalPlayer) {
			enabled = false;
			GetComponent<Camera>().enabled = false;
		}
	}

	private void FixedUpdate() {
		transform.position += transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
	}
}
