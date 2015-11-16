using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

		Transform s1 = common.CreateShotPosition();

		yield return new WaitForEndOfFrame();
		while (true) 
		{
			s1.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60));
			spaceship.Shot(s1,1,2,BulletManager.BulletType.Slash);
						
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
