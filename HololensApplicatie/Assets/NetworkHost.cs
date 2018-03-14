using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHost : NetworkManager {

    public static NetworkHost instance;

	public int playerCount;

	[System.Serializable]
	public class Server_Init : UnityEvent<string> { }
	
	[SerializeField]
	InputField ipField;

	public Server_Init server_Init = new Server_Init();
	public UnityEvent Server_Connected = new UnityEvent();

    public static string IPPort {
        get {
            return instance.networkAddress + ":" + instance.networkPort;
        }
    }

    private void Start() {
        instance = this;
    }

    public void StartupServer() {
		StartHost();
	}

	public override void OnStartServer() {
		server_Init.Invoke(networkAddress + ":" + networkPort);
		Debug.Log("Server ready on " + networkAddress + ":" + networkPort  + "!");
	}

	public override void OnClientConnect(NetworkConnection connection) {
		Server_Connected.Invoke();
		Debug.Log("Connected to server!");
	}

	public override void OnServerConnect(NetworkConnection connection) {
		Debug.Log("Player connected from " + connection.address + "!");
		playerCount++;
	}

	public override void OnServerDisconnect(NetworkConnection connection) {
		Debug.Log("Player disconnected from " + connection.address + "!");
		playerCount--;
	}

	public void Connect() {
		Connect(ipField.text);
	}

	public void Connect(string ip) {
		networkAddress = ip;

		StartClient();
	}
}
