using UnityEngine;
using System.Collections;

public class BossChampion_2 : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	public int power=2;
    public int speed = 2;
	
	Transform s2;
	Transform pt;

    public GameObject darksun;
	
	//SE関係
	public AudioClip shootSE;
	public AudioClip skillSE;
	//public AudioClip shootSE2;
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

		FindObjectOfType<MessageWindow>().showMessage("メテオ");
		
		yield return new WaitForEndOfFrame();
		
		StartCoroutine("Stop");
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");
		
		yield break;
	}
	
	IEnumerator Stop()
	{
		FindObjectOfType<MessageWindow>().showMessage("リターンマッチ！");
		yield return new WaitForSeconds(2);
		enemy.MoveStop();
	}
	
	IEnumerator Attack1(){//
		yield return new WaitForSeconds(2);

		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「待っていたぞ…」");
		yield return new WaitForSeconds(2);
		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「再び戦えるこの時をっ！！」");
		yield return new WaitForSeconds(2);

		while (true) 
		{
            FindObjectOfType<MessageWindow>().showMessage("「俺は強くなった！」");
			for(int i=0; i<8; ++i){
				audioSource.PlayOneShot(shootSE);
				int r = Random.Range(0,40);
				for (int n=0; n<18;++n )
				{
					common.Shot(s2,r+n*40,5,4,BulletManager.BulletType.GrandSlash);
				}
                common.Shot(s2, Random.Range(0, 180), 2, 3, BulletManager.BulletType.DarkChaser);
				
				//shotDelay秒待つ
				yield return new WaitForSeconds(0.2f);
			}

			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「この技で！」");
			for(int i=0; i<6; ++i)
            {
                for (int n = 0; n < 12; ++n)
                {
                    common.Shot(s2, 15*n, 2, 3, BulletManager.BulletType.DarkChaser);
                }
                yield return new WaitForSeconds(1.0f);
			}

			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「必ずや返り咲く！」");
			for(int i=0; i<40; ++i)
            {
				common.Shot(s2,Random.Range(45,135),2,3,BulletManager.BulletType.SlashBullet,0.5f,1);
				common.Shot(s2,Random.Range(45,135),2,3,BulletManager.BulletType.SlashBullet,0.5f,-1);
				yield return new WaitForSeconds(0.15f);
			}

            spaceship.GetAnimator().SetTrigger("Skill");
            FindObjectOfType<MessageWindow>().showMessage("「見よ！」");
            yield return new WaitForSeconds(1.0f);

            spaceship.GetAnimator().SetTrigger("Skill");
            FindObjectOfType<MessageWindow>().showMessage("闇の力が満ちる…");
            yield return new WaitForSeconds(1.0f);

			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「この力を…！」");
			yield return new WaitForSeconds(1.0f);
            
            GameObject g = (GameObject)Instantiate(darksun, s2.position, Quaternion.identity); 
            yield return new WaitForSeconds(8f);
		}
		//common.ShotAim(s2, pt, power, 0, BulletManager.BulletType.CircleLeaf);
	}

	void Update () {
		
	}

}
