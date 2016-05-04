using UnityEngine;
using System.Collections;

public class BulletStar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,180*Time.deltaTime);
	}
}
