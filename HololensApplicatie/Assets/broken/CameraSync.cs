using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CameraSync : NetworkBehaviour {

	public WebCamTexture camTexture { get; private set; }
	public RawImage rawImage;
	public Texture2D tex { get; private set; }

	public static CameraSync instance;

	private void Awake() {
		if(isLocalPlayer)
			instance = this;
	}

	private void Start() {
		tex = rawImage.texture as Texture2D;

		if(!isServer) {
			enabled = false;
			return;	
		}
		camTexture = new WebCamTexture();
		rawImage.texture = camTexture;
		camTexture.requestedFPS = 22;
		camTexture.requestedHeight = 50;
		camTexture.requestedWidth = 50;
		camTexture.Play();
	}

	private void FixedUpdate() {
		if(isServer) {
			CameraSyncReceiver.instance.ReceiveImage(camTexture.GetPixels());
			tex.SetPixels(camTexture.GetPixels());
			tex.Apply();
			//tex.SetPixels(camTexture.GetPixels());
			//ReceiveImage(tex.EncodeToJPG());
		}
	}
}
