using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour 
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 3;
    Enemy enemy;

    //SE関係
    public AudioClip shootSE;
    //public AudioClip skillSE;
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
        StartCoroutine("Attack1");

        yield break;
	}

    IEnumerator Stop()
    {
        //yield return new WaitForSeconds(1.0f);
        while (this.transform.position.x > 3.0f)
        { yield return new WaitForSeconds(1.0f); }

        //audioSource.PlayOneShot(shootSE);
        enemy.MoveStop();
        //enemy.MoveAim(transform.position, pt.position, 4);
        yield return null;
    }

    IEnumerator Attack1()
    {

        while (true)
        {
            audioSource.PlayOneShot(shootSE);
            common.ShotAim(s1, pt, power, speed, BulletManager.BulletType.SlashBullet);

            yield return new WaitForSeconds(spaceship.shotDelay);
        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}
