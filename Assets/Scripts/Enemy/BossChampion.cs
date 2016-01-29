using UnityEngine;
using System.Collections;

public class BossChampion : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	public int power=2;
	
	Transform s2;
	Transform pt;
	
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
		FindObjectOfType<MessageWindow>().showMessage("チャンピオン");
		yield return new WaitForSeconds(2);
		enemy.MoveStop();
	}
	
	IEnumerator Attack1(){//
		yield return new WaitForSeconds(2);

		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「よくぞここまで来たものだ」");
		yield return new WaitForSeconds(2);
		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「では私自らが試してやろう」");
		yield return new WaitForSeconds(2);

		while (true) 
		{

			for(int i=0; i<8; ++i){
				audioSource.PlayOneShot(shootSE);
				int r = Random.Range(0,40);
				for (int n=0; n<9;++n )
				{

					common.Shot(s2,r+n*40,5,4,BulletManager.BulletType.GrandSlash);

				}
				
				//shotDelay秒待つ
				yield return new WaitForSeconds(0.7f);
			}

			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「これはどうかな？」");
			for(int i=0; i<6; ++i){
				common.Shot(s2,Random.Range(0,180),2,3,BulletManager.BulletType.DarkChaser);
				yield return new WaitForSeconds(1.0f);
			}
			for(int i=0; i<16; ++i){
				common.Shot(s2,Random.Range(0,180),2,3,BulletManager.BulletType.DarkChaser);
				yield return new WaitForSeconds(0.5f);
			}


			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「これはかわせまい」");
			for(int i=0; i<80; ++i){
				common.Shot(s2,Random.Range(45,135),2,3,BulletManager.BulletType.SlashBullet,0.5f,1);
				common.Shot(s2,Random.Range(45,135),2,3,BulletManager.BulletType.SlashBullet,0.5f,-1);
				yield return new WaitForSeconds(0.15f);
			}

			spaceship.GetAnimator().SetTrigger("Skill");
			FindObjectOfType<MessageWindow>().showMessage("「消え失せよ！」");
			yield return new WaitForSeconds(0.7f);
			for(int i=0; i<3; ++i){
				common.ShotAim(s2,pt,10,5,BulletManager.BulletType.DarknessCore);
				yield return new WaitForSeconds(1f);
			}
		}
		//common.ShotAim(s2, pt, power, 0, BulletManager.BulletType.CircleLeaf);
	}

	void Update () {
		
	}

}
