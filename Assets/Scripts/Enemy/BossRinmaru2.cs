using UnityEngine;
using System.Collections;

public class BossRinmaru2 : MonoBehaviour {
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

		//FindObjectOfType<MessageWindow>().showMessage("メテオ");

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
        yield return new WaitForSeconds(3.0f);
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
        FindObjectOfType<MessageWindow>().showMessage("歩兵突撃！");

        yield return null;
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
