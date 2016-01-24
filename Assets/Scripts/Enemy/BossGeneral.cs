using UnityEngine;
using System.Collections;

public class BossGeneral : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;
    public int shotSpeed = 3;

	Transform s2;
	//Transform pt;

    //SE関係
    //public AudioClip shootSE;
    public AudioClip skillSE;
    AudioSource audioSource;

    //召喚するやつ
    public GameObject soldier;
    public GameObject archer;
    public GameObject armor;
	
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
		//pt = FindObjectOfType<Party>().transform;

		FindObjectOfType<MessageWindow>().showMessage("メテオ");

		yield return new WaitForEndOfFrame();

        FindObjectOfType<MessageWindow>().showMessage("ジェネラル！");

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

        for (int n = 0; n < 5; ++n)
        {
            float xoffset = 1.4f;
            float yoffset = 1.0f;
            Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
            Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

            //common.ShotAim(s2, pt, power, shotSpeed, BulletManager.BulletType.BananaSlash);

            //shotDelay秒待つ
            yield return new WaitForSeconds(1.5f);
        }

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("弓兵、構え！");

        {
            float xoffset = 1.4f;
            float yoffset = 1.0f;
            Instantiate(archer, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
            //Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(archer, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

        }

        yield return new WaitForSeconds(2.0f);

        //以下ループ行動
		while (true) 
		{
            if (!GameObject.Find("EnemyArmor(Clone)")
                && !GameObject.Find("EnemyArmorZero(Clone)"))
            {
                spaceship.GetAnimator().SetTrigger("Skill");
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("重騎士、前へ！");

                float xoffset = 1.4f;
                float yoffset = 0.3f;
                Instantiate(armor, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
                //Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
                Instantiate(armor, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
            }
            else if (!GameObject.Find("EnemyArcher(Clone)")
                && !GameObject.Find("EnemyArcherZero(Clone)"))
            {
                spaceship.GetAnimator().SetTrigger("Skill");
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("弓兵、構え！");

                {
                    float xoffset = 1.4f;
                    float yoffset = 1.0f;
                    Instantiate(archer, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
                    //Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
                    Instantiate(archer, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

                }

                
            }
            else
            {
                spaceship.GetAnimator().SetTrigger("Skill");
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("歩兵突撃！");

                for (int n = 0; n < 5; ++n)
                {
                    float xoffset = 1.4f;
                    float yoffset = 1.0f;
                    Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
                    Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
                    Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

                    //common.ShotAim(s2, pt, power, shotSpeed, BulletManager.BulletType.BananaSlash);

                    //shotDelay秒待つ
                    yield return new WaitForSeconds(1.5f);
                }
            }

            yield return new WaitForSeconds(2.0f);

          
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
