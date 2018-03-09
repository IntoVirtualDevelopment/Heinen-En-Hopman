using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSender : NetworkBehaviour {

	WebCamTexture wct;
	Texture2D t2d;

	private void Start() {
		if(!isServer)
			return;

		wct = new WebCamTexture();
		wct.requestedWidth = 50;
		wct.requestedHeight = 50;
		wct.requestedFPS = 22;

		t2d = new Texture2D(50, 50);
	}

	private void FixedUpdate() {
		t2d.SetPixels(wct.GetPixels());
		t2d.Apply();
		CameraReceiver.instance.Receive(t2d.EncodeToJPG());
	}
}
