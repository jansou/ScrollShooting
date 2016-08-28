using UnityEngine;
using System.Collections;

public class StoneAlex : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;
    public int shotPower = 5;

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
			for(int i=0; i<10; ++i){
				common.ShotNWay(st,90,shotPower,3,BulletManager.BulletType.SlashBullet,3,30);
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
