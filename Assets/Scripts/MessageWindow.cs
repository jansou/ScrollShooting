using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour {
	Text text;
	Canvas canvas;

	int displayFrame = 0;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		text = transform.FindChild("Text").GetComponent<Text>();
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(displayFrame > 0){
			--displayFrame;
			if(displayFrame == 0){
				canvas.enabled = false;
			}
		}
	
	}

	public void showMessage(string message){
		text.text = message;
		canvas.enabled = true;
		displayFrame = 120;
	}

}
