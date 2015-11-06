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
			Vector3 v = Vector3.left;
			if(pt){
				v = pt.position - transform.position;
			}
			s2.localRotation = Quaternion.FromToRotation(Vector3.up,v);
			spaceship.Shot(s2,1,3,BulletManager.BulletType.Slash);

			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay + 0.5f);
		}
	}
	IEnumerator Attack2(){
		while (true) 
		{
			for(int i=0; i<3; ++i){
				s2.localRotation = Quaternion.Euler(0,0,60+30*i);
				spaceship.Shot(s2,1,2);
			}
			
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
