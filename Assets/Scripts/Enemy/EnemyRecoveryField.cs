using UnityEngine;
using System.Collections;

public class EnemyRecoveryField : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if(Time.timeScale != 0 && col.tag == "Enemy"){
			col.GetComponent<Enemy>().RecoveryHP(1);
		}
	}
}
