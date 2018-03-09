using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraReceiver :   {
	public static CameraReceiver instance;

	private void Start() {
		instance = this;
	}

	[Client]
	public void Receive(byte[] pixels) {

	}
}
