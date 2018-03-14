using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

public class DebugHololensInput : MonoBehaviour {

	GestureRecognizer gr;

	void Start() {
		gr = new GestureRecognizer();

		gr.Tapped += OnTap;

		gr.StartCapturingGestures();
	}

	void OnTap(TappedEventArgs args) {
        
	}
}
