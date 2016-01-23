using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour 
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 3;
    Enemy enemy;

    Transform s1;
    Transform pt;

	IEnumerator Start () 
    {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

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
            common.ShotAimNway(s1, pt, power, speed, BulletManager.BulletType.BananaSlash,3,20);


            yield return new WaitForSeconds(spaceship.shotDelay);
        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}
