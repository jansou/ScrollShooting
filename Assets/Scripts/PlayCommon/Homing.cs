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
			Vector3 p = transform.position;
			int aimi = 0;
			float minDistance = Vector3.Distance(p,enemies[0].transform.position);
			for(int i=1; i<enemies.Length; ++i){
				if(Vector3.Distance(p,enemies[i].transform.position) < minDistance){
					aimi = i;
				}
			}
			Vector3 ep = enemies[aimi].transform.position;
			Vector3 v = ep - transform.position;
			transform.rotation = Quaternion.FromToRotation(Vector3.up,v);
			GetComponent<Rigidbody2D>().velocity = transform.up.normalized * 5;
		}
	}
}
