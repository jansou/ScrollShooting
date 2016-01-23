using UnityEngine;
using System.Collections;

public class BulletLeaf : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,Time.deltaTime*calcSpeed(160,0.8f),0);
		transform.Rotate(0,0,160*Time.deltaTime);
	}
	float calcSpeed(float rot,float rad){
		return 2 * Mathf.PI* rad * rot / 360;
	}
}
