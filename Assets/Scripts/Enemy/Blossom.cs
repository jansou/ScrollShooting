using UnityEngine;
using System.Collections;

public class Blossom : MonoBehaviour
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 2;
	
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

		Transform s1 = common.CreateShotPosition();

		yield return new WaitForEndOfFrame();
		while (true) 
		{
            common.Shot(s1, 0+30, power, speed, BulletManager.BulletType.BlossomBullet);
            common.Shot(s1, 60 + 30, power, speed, BulletManager.BulletType.BlossomBullet);
            common.Shot(s1, 120 + 30, power, speed, BulletManager.BulletType.BlossomBullet);
            common.Shot(s1, 180 + 30, power, speed, BulletManager.BulletType.BlossomBullet);
            common.Shot(s1, 240 + 30, power, speed, BulletManager.BulletType.BlossomBullet);
            common.Shot(s1, 300 + 30, power, speed, BulletManager.BulletType.BlossomBullet);

			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
