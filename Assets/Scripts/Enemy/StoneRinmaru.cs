using UnityEngine;
using System.Collections;

public class StoneRinmaru : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;
	
	// Use this for initialization
	IEnumerator  Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();
		
		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;
		
		yield return new WaitForSeconds(2.0f);
		
		StartCoroutine("Attack");
	}
	
	IEnumerator Attack(){
		while(true){
			yield return new WaitForSeconds(4.0f);
			for(int i=0; i<20; ++i){
				common.ShotNWay(st,90+i*10,5,7,BulletManager.BulletType.SlashBullet,4,90);
				yield return new WaitForSeconds(0.2f);
			}
			yield return new WaitForSeconds(9.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
