using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CameraSyncReceiver : NetworkBehaviour {

	public static CameraSyncReceiver instance;
	public RawImage rawImage;

	Texture2D tex;

	private void Start() {
		instance = this;

		tex = new Texture2D(50, 50);
		rawImage.texture = tex;
	}

	[Client]
	public void ReceiveImage(Color[] pixels) {
		tex.SetPixels(pixels);
		tex.Apply();
		print("i have pixels");
	}
}
