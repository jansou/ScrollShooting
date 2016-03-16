using UnityEngine;
using System.Collections;

public class Une : MonoBehaviour
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 2;

    //SE関係
    public AudioClip shootSE;
    //public AudioClip skillSE;
    AudioSource audioSource;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

		Transform s1 = common.CreateShotPosition();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shootSE;
        //

		yield return new WaitForEndOfFrame();
		while (true) 
		{
            audioSource.PlayOneShot(shootSE);
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
