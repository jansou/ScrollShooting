using UnityEngine;
using System.Collections;

public class DummyBom : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;
    public int shotSpeed = 5;

	Transform s2;
	Transform pt;

    //SE関係
    public AudioClip shootSE;
    public AudioClip skillSE;
    AudioSource audioSource;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shootSE;
        //

		s2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		//FindObjectOfType<MessageWindow>().showMessage("メテオ");

		yield return new WaitForEndOfFrame();

        //StartCoroutine("Stop");
		
		StartCoroutine("Attack1");

		yield break;
	}

    IEnumerator Stop()
    {
        while (this.transform.position.x > 3.0f)
        { yield return new WaitForSeconds(1.0f); }
        //yield return new WaitForSeconds(2);
        enemy.MoveStop();
        StartCoroutine("Attack1");
    }

	IEnumerator Attack1()
    {//
        /*
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("3");
        spaceship.GetAnimator().SetTrigger("Skill");
        yield return new WaitForSeconds(2);

        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("2");
        spaceship.GetAnimator().SetTrigger("Skill");
        yield return new WaitForSeconds(2);

        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("1");
        spaceship.GetAnimator().SetTrigger("Skill");
        yield return new WaitForSeconds(2);
        */
        //for (int i=0;i<5 ;++i )
        while (true)
        {
            if (enemy.hp < enemy.maxHP / 2)
            {
                //audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("崩落！");
                //spaceship.GetAnimator().SetTrigger("Skill");
                //yield return new WaitForSeconds(0.5f);
                common.ShotStoneFall(s2, shotSpeed, power, BulletManager.BulletType.RockBullet);
                enemy.ExplodeSelf();

            }
            yield return new WaitForSeconds(0);
        }
	}
    /*
	IEnumerator Attack2(){//3way
		while (true) 
		{
			for(int i=0; i<3; ++i){
				common.Shot(s2, 60+30*i,1,2);
			}
			
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	*/
	// Update is called once per frame
	void Update () {
		
	}
}
