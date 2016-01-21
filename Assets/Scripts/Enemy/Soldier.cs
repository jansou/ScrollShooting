using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {
	//Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	Transform pt;

	IEnumerator Start () {
		//spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

		pt = FindObjectOfType<Party>().transform;
		//Transform s1 = common.CreateShotPosition();

		yield return new WaitForEndOfFrame();


		StartCoroutine("AimRun");

		/*
		while (true) 
		{
            //キャラのいる位置まで突進する
            common.Shot(s1, 60 + Random.Range(0, 60), power, 1, BulletManager.BulletType.SlimeBullet);
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
		*/
	}

	IEnumerator AimRun(){
		yield return new WaitForSeconds(1.0f);
		enemy.MoveAim(transform.position,pt.position,4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
