using UnityEngine;
using System.Collections;

public class BossDemo : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;
    public int shotSpeed = 3;
    private int count=0;

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
        audioSource.clip = skillSE;
        //

		s2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
        yield return new WaitForSeconds(4.0f);
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");

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
    {//
        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「ふふふ…よくぞここまで来たな勇者よ」");
        yield return new WaitForSeconds(3.0f);

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「その努力だけは誉めてやろう」");
        yield return new WaitForSeconds(3.0f);

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「だがここで終わりだ」");
        yield return new WaitForSeconds(3.0f);

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("闇の力が満ちてゆく…");
        yield return new WaitForSeconds(3.0f);

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「消え失せよ！」");
        yield return new WaitForSeconds(3.0f);

        audioSource.PlayOneShot(shootSE);
        common.ShotAim(s2, pt, power, shotSpeed, BulletManager.BulletType.DarknessCore);

        yield return new WaitForSeconds(0.8f);


        FindObjectOfType<MessageWindow>().showMessage("勇者「うわ、あぶね！」");
        yield return new WaitForSeconds(3.0f);

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「……」");
        yield return new WaitForSeconds(3.0f);
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
