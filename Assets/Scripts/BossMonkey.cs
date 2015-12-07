using UnityEngine;
using System.Collections;

public class BossMonkey : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;

	Transform s2;
	Transform pt;
	
	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

		s2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();

		StartCoroutine("Attack1");
		StartCoroutine("Attack2");

		yield break;
	}

	IEnumerator Attack1(){
		while (true) 
		{
			common.ShotAim(s2,pt,2,3,BulletManager.BulletType.Slash);

			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay + 0.5f);
		}
	}
	IEnumerator Attack2(){
		while (true) 
		{
			for(int i=0; i<3; ++i){
				common.Shot(s2, 60+30*i,1,2);
			}
			
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
