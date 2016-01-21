using UnityEngine;
using System.Collections;

public class BulletLeaf : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,calcSpeed(3,0.8f),0);
		transform.Rotate(0,0,3);
	}
	float calcSpeed(float rot,float rad){
		return 2 * Mathf.PI* rad * rot / 360;
	}
}
