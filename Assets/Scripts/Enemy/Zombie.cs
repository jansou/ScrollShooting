using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour 
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 3;
    Enemy enemy;

    //SE関係
    public AudioClip shootSE;
    //public AudioClip shootSE2;
    public AudioClip skillSE;
    AudioSource audioSource;

    Transform s1;
    Transform pt;

	IEnumerator Start () 
    {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = shootSE;
        //

		s1 = common.CreateShotPosition();
        pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
        

        yield break;
	}

    IEnumerator Stop()
    {
        //yield return new WaitForSeconds(1.0f);
        while (this.transform.position.x >4.0f)//4.0がちょうど画面はじ
        { yield return new WaitForSeconds(1.0f); }

        //audioSource.PlayOneShot(shootSE);
        enemy.MoveStop();
        //spaceship.speed *= 2;
        StartCoroutine("Attack1");
        //enemy.MoveAim(transform.position, pt.position, 4);
        yield return null;
    }

    IEnumerator Attack1()
    {

        while (true)
        {

            AimRun();

            yield return new WaitForSeconds(0.5f);

            //enemy.MoveStop();

            //audioSource.PlayOneShot(shootSE);
            //common.ShotAim(s1, pt, power, speed, BulletManager.BulletType.BananaSlash);

            //common.ShotAim(s1, pt, power, speed, BulletManager.BulletType.SlashBullet);
            //common.Shot(s1, pt, power, speed, BulletManager.BulletType.SlashBullet);
            //common.ShotAimNway(s1, pt, power, speed, BulletManager.BulletType.AllowBullet, 2, 20);

            //common.Shot(s1, 30, power, speed, BulletManager.BulletType.SlashBullet);
            //common.Shot(s1, 60 + 30, power, speed, BulletManager.BulletType.SlashBullet);
            //common.Shot(s1, 120 + 30, power, speed, BulletManager.BulletType.SlashBullet);

            yield return new WaitForSeconds(1.0f);
        }

    }

    void AimRun()
    {
		if(pt){
        	audioSource.PlayOneShot(skillSE);
        	
			enemy.MoveAim(transform.position,pt.position,4);
		}
        
	}

	// Update is called once per frame
	void Update () {
	
	}
}
