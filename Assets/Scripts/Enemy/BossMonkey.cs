using UnityEngine;
using System.Collections;

public class BossMonkey : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;

	Transform s2;
	Transform pt;

	public bool isNormalEnemy = false;//雑魚として扱う場合にチェック

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

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");

		yield break;
	}

    IEnumerator Stop()
    {
        if(!isNormalEnemy){
			FindObjectOfType<MessageWindow>().showMessage("ボスザル");
		}
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

	IEnumerator Attack1(){//banana
		while (true) 
		{
            for (int n=0; n<10;++n )
            {
                audioSource.PlayOneShot(shootSE);
                common.ShotAim(s2, pt, power, 3, BulletManager.BulletType.BananaSlash);
                
                //shotDelay秒待つ
                yield return new WaitForSeconds(spaceship.shotDelay);
            }

            //audioSource.clip = skillSE;
            audioSource.PlayOneShot(skillSE);
			if(!isNormalEnemy){
            	FindObjectOfType<MessageWindow>().showMessage("バナナラッシュ！");
			}

			spaceship.GetAnimator().SetTrigger("Skill");
			yield return new WaitForSeconds(0.5f);

            //バナナラッシュ
            for (int m = 0; m < 2; ++m)
            {
                for (int n = 0; n < 4; ++n)
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        audioSource.PlayOneShot(shootSE);
                        common.Shot(s2, -45+ 75*m + 30 * i, power, 4 - n, BulletManager.BulletType.BananaSlash);
                    }

                }
                yield return new WaitForSeconds(spaceship.shotDelay + 1.0f);
            }

			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay + 2.5f);
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
