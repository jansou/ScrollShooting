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
		Transform s2 = common.CreateShotPosition();
		
		while (true) 
		{
			s1.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60));
			spaceship.Shot(s1,1);
			s2.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60));
			spaceship.Shot(s2,1);
						
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
