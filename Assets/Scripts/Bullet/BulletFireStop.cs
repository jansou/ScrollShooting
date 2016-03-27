using UnityEngine;
using System.Collections;

public class BulletFireStop : MonoBehaviour {
	public float stopTime;
	float nowTime = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		nowTime += Time.deltaTime;
		if(nowTime >= stopTime){
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
