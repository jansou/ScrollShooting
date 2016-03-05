using UnityEngine;
using System.Collections;

public class RockGoblin : MonoBehaviour 
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
        while (this.transform.position.x > 2.0f)
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
            //common.ShotAim(s1, pt, power, speed, BulletManager.BulletType.SlashBullet);
            //common.Shot(s1, pt, power, speed, BulletManager.BulletType.SlashBullet);
            //common.ShotAimNway(s1, pt, power, speed, BulletManager.BulletType.AllowBullet, 2, 20);

            //common.Shot(s1, 30, power, speed, BulletManager.BulletType.RockBullet);

			ShotThrowAim(s1,pt,power,speed,BulletManager.BulletType.RockBullet);
            yield return new WaitForSeconds(spaceship.shotDelay);
        }

    }

	public void ShotThrowAim(Transform origin,Transform aim, int shotPower,int shotSpeed, BulletManager.BulletType type){
		if(aim){
			Vector3 d = aim.position - transform.position;
			Vector3 v = Vector3.zero;
			v.x = d.x / 2;
			v.y = d.y / 2 + 0.5f * 2 * 3f;
			//v.y = d.y / 10;
			origin.localRotation = Quaternion.FromToRotation(Vector3.up,v);
			spaceship.Shot(origin,shotPower,v.magnitude,type);
		}
		else{
			common.Shot (origin,90,shotPower,shotSpeed,type);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
