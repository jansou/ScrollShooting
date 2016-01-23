using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	Transform pt;

    //SE関係
    public AudioClip shootSE;
    //public AudioClip skillSE;
    AudioSource audioSource;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shootSE;
        //

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
		//yield return new WaitForSeconds(1.0f);
        while (this.transform.position.x > 3.0f) 
        { yield return new WaitForSeconds(1.0f); }
        
        audioSource.PlayOneShot(shootSE);
        spaceship.speed *= 2;
		enemy.MoveAim(transform.position,pt.position,4);
        yield return null;
	}
	
	// Update is called once per frame
	void Update () {
        //if (transform.position.x < 2.0f)  StartCoroutine("AimRun");
	
	}
}
