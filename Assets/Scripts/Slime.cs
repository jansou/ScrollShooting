using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();


		Transform s1 = common.CreateShotPosition();

		while (true) 
		{
			s1.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60));
			spaceship.Shot(s1,1);
			
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
