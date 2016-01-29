using UnityEngine;
using System.Collections;

public class Hisoji : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	public int power=2;
    public int speed = 2;
	
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
		FindObjectOfType<MessageWindow>().showMessage("プログラマー：hisoji");
		yield return new WaitForSeconds(2);
		enemy.MoveStop();
	}
	
	IEnumerator Attack1(){//
		yield return new WaitForSeconds(2);

		spaceship.GetAnimator().SetTrigger("Skill");
        FindObjectOfType<MessageWindow>().showMessage("「遊んでくれてありがとうございました。」");
		yield return new WaitForSeconds(2);
		spaceship.GetAnimator().SetTrigger("Skill");
        FindObjectOfType<MessageWindow>().showMessage("「これから続いていくマーチへ、」");
		yield return new WaitForSeconds(2);
        spaceship.GetAnimator().SetTrigger("Skill");
        FindObjectOfType<MessageWindow>().showMessage("「また会いに来てください」");
        yield return new WaitForSeconds(2);

        spaceship.GetAnimator().SetTrigger("Skill");
        FindObjectOfType<MessageWindow>().showMessage("スラッシュが好き");

		while (true)
        {
            int a=Random.Range(1, 10);

            common.ShotAimNway(s2, pt, power, speed*a, BulletManager.BulletType.SlashBullet, 3, 20);

            yield return new WaitForSeconds(2);
		}
		//common.ShotAim(s2, pt, power, 0, BulletManager.BulletType.CircleLeaf);
	}

	void Update () {
		
	}

}
