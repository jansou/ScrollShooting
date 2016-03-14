using UnityEngine;
using System.Collections;

public class Jansou : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	public int power=2;
    public int speed = 2;
	
	Transform s2;
	//Transform pt;
	
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
		//pt = FindObjectOfType<Party>().transform;
		
		FindObjectOfType<MessageWindow>().showMessage("メテオ");
		
		yield return new WaitForEndOfFrame();
		
		StartCoroutine("Stop");
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");
		
		yield break;
	}
	
	IEnumerator Stop()
	{
		FindObjectOfType<MessageWindow>().showMessage("その他：じゃんそう");
		yield return new WaitForSeconds(2);
		enemy.MoveStop();
	}
	
	IEnumerator Attack1(){//
		yield return new WaitForSeconds(2);

		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「ここまで遊んでくれてありがとう!」");
		yield return new WaitForSeconds(2);
		spaceship.GetAnimator().SetTrigger("Skill");
		FindObjectOfType<MessageWindow>().showMessage("「4章以降は完全版として制作予定なので」");
		yield return new WaitForSeconds(2);
        spaceship.GetAnimator().SetTrigger("Skill");
        FindObjectOfType<MessageWindow>().showMessage("「その時はまた遊んでね」");
        yield return new WaitForSeconds(2);

		while (true)
        {
            spaceship.GetAnimator().SetTrigger("Skill");
            FindObjectOfType<MessageWindow>().showMessage("葉っぱ好き");
            for (int m = 0; m < 6; ++m)
            {
                audioSource.PlayOneShot(shootSE);

               // int a = Random.Range(0, 6);
                //float face = (a % 2 == 0) ? 1.0f : -1.0f;
                //face = (a % 3 == 0) ? 0.5f : face;

                for (int n = 0; n < 6; ++n)
                {
                    //common.Shot(s2, 90, power, 4, BulletManager.BulletType.LeafBullet, 1.0f, n * 0.3f * face + transform.position.y);
                    
                    for (int i = 0; i < 6; ++i)
                    {
                    
                        int angle = -45 + 75 * m + 30 * i;
                        common.Shot(s2,angle, power, 4 - n, BulletManager.BulletType.LeafBullet,1,1);
                    }

                }
                yield return new WaitForSeconds(spaceship.shotDelay + 0.3f);
            }
		}
		//common.ShotAim(s2, pt, power, 0, BulletManager.BulletType.CircleLeaf);
	}

	void Update () {
		
	}

}
