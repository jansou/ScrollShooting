using UnityEngine;
using System.Collections;

public class BossGeneral : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;
    public int shotSpeed = 3;

	Transform s2;
	Transform pt;

    //召喚するやつ
    public GameObject soldier;
	
	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

		s2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		FindObjectOfType<MessageWindow>().showMessage("メテオ");

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");

		yield break;
	}

    IEnumerator Stop()
    {
        FindObjectOfType<MessageWindow>().showMessage("ボスザル");
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

	IEnumerator Attack1(){//banana
		while (true) 
		{
            for (int n=0; n<10;++n )
            {
                Instantiate(soldier, transform.position, Quaternion.identity);

                //common.ShotAim(s2, pt, power, shotSpeed, BulletManager.BulletType.BananaSlash);
                
                //shotDelay秒待つ
                yield return new WaitForSeconds(3.0f);
            }



            FindObjectOfType<MessageWindow>().showMessage("バナナラッシュ！");

			spaceship.GetAnimator().SetTrigger("Skill");
			yield return new WaitForSeconds(0.5f);

            for (int m = 0; m < 2; ++m)
            {
                for (int n = 0; n < 4; ++n)
                {
                    for (int i = 0; i < 6; ++i)
                    {
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
