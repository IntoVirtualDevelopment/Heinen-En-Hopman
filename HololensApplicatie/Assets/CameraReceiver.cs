using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraReceiver : NetworkBehaviour {
	
	public static CameraReceiver instance;

	public Texture2D texture;

	private void Start() {
        instance = this;
        texture = new Texture2D(50, 50);
	}

	[Client]
	public void Receive(byte[] pixels) {
		texture.LoadRawTextureData(pixels);
        texture.Apply();
	}
}
