using UnityEngine;
using System.Collections;

public class EnemyRecoveryField : MonoBehaviour {

    int framecount = 0;
    public int healTimingCount=5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ++framecount;
	}

	void OnTriggerStay2D(Collider2D col){
		if(Time.timeScale != 0 && col.tag == "Enemy")
        {
            if (framecount == healTimingCount)
			col.GetComponent<Enemy>().RecoveryHP(1);
		}
	}
	void OnTriggerEnter2D(Collider2D col){

	}
}
