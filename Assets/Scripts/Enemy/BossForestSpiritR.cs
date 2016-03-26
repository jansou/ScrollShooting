using UnityEngine;
using System.Collections;

public class BossForestSpiritR : MonoBehaviour
{
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;
	
    public int power=2;
	public bool isNormalEnemy = false;

	Transform s2;
	Transform pt;

    //SE関係
    public AudioClip shootSE;
    public AudioClip skillSE;
    public AudioClip shootSE2;
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
			FindObjectOfType<MessageWindow>().showMessage("森の精霊");
		}
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

	IEnumerator Attack1(){//
		while (true) 
		{
            
            for (int n=0; n<4;++n )
            {
                audioSource.PlayOneShot(shootSE);
                common.ShotAim(s2, pt, power, 0, BulletManager.BulletType.CircleLeaf);
                
                //shotDelay秒待つ
                yield return new WaitForSeconds(spaceship.shotDelay+1.5f);
            }
            

			if(!isNormalEnemy){
	            FindObjectOfType<MessageWindow>().showMessage("コガラシ！");
			}
			
			audioSource.PlayOneShot(skillSE);

			spaceship.GetAnimator().SetTrigger("Skill");
			yield return new WaitForSeconds(0.5f);

            
            for (int m = 0; m < 6; ++m)
            {
                audioSource.PlayOneShot(shootSE2);

                int a = Random.Range(0, 6);
                float face = (a%2==0)?1.0f:-1.0f;
                //face = (a % 3 == 0) ? 0.5f : face;

                for (int n = 0; n < 6; ++n)
                {
                    common.Shot(s2, 90, power, 4, BulletManager.BulletType.LeafBullet,1.0f,n*0.3f*face+transform.position.y);
                    //common.Shot(s2, angle, power, 4 - n, BulletManager.BulletType.LeafBullet, 1, 1);

                    //for (int i = 0; i < 6; ++i)
                    //{
                    //    int angle = -45 + 75 * m + 30 * i;
                    //    common.Shot(s2,angle, power, 4 - n, BulletManager.BulletType.LeafBullet,1,1);
                    //}

                }
                yield return new WaitForSeconds(spaceship.shotDelay+0.3f);
            }

			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay + 2.0f);
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
