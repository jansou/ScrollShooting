using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if(enemies.Length == 0){
			transform.rotation = Quaternion.AngleAxis(270,Vector3.forward);
		}
		else{
			Vector3 ep = enemies[0].transform.position;
			Vector3 v = ep - transform.position;
			transform.rotation = Quaternion.FromToRotation(Vector3.up,v);
			GetComponent<Rigidbody2D>().velocity = transform.up.normalized * 5;
		}
	}
}
