using UnityEngine;
using System.Collections;

public class BossGiftman : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    public int power=2;
    public int speed = 3;

	Transform s2;
	Transform pt;

    //SE関係
    public AudioClip shootSE;
    public AudioClip shootSE2;
    public AudioClip skillSE;
    AudioSource audioSource;
    public GameObject stone;
    public GameObject gob;

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

		//FindObjectOfType<MessageWindow>().showMessage("メテオ");
        FindObjectOfType<MessageWindow>().showMessage("岩の魔獣！");

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");

		yield break;
	}

    IEnumerator Stop()
    {
        while (this.transform.position.x > 3.0f)
        { yield return new WaitForSeconds(1.0f); }

        //FindObjectOfType<MessageWindow>().showMessage("岩の魔獣！");
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

    public void ShotThrowAim(Transform origin, Transform aim, int shotPower, int shotSpeed, BulletManager.BulletType type)
    {
        if (aim)
        {
            Vector3 d = aim.position - transform.position;
            Vector3 v = Vector3.zero;
            v.x = d.x / 2;
            v.y = d.y / 2 + 0.5f * 2 * 3f;
            //v.y = d.y / 10;
            origin.localRotation = Quaternion.FromToRotation(Vector3.up, v);
            spaceship.Shot(origin, shotPower, v.magnitude, type);
           
        }
        else
        {
            common.Shot(origin, 90, shotPower, shotSpeed, type);
        }
    }

	IEnumerator Attack1()
    {
        while (true)
        {
            for (int n = 0; n < 5; ++n)
            {
                audioSource.PlayOneShot(shootSE);
                ShotThrowAim(s2, pt, power, speed, BulletManager.BulletType.RockBullet);
                yield return new WaitForSeconds(1.0f);
            }

            //
            audioSource.PlayOneShot(skillSE);
            FindObjectOfType<MessageWindow>().showMessage("グロロロロロ……");
            spaceship.GetAnimator().SetTrigger("Skill");

            for (int i = 0; i < 5; ++i)
            {
                audioSource.PlayOneShot(shootSE2);
                common.ShotStoneFall(s2, speed, power, BulletManager.BulletType.RockBullet);
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.0f);

            //if(enemy.hp<enemy.maxHP/2)
            {
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("ゴガガガガガガ……！");
                spaceship.GetAnimator().SetTrigger("Skill");

                for (int i = 0; i < 6; ++i)
                {
                    float xoffset = 1.4f;
                    float yoffset = Random.Range(-1.5f,1.5f);
                    audioSource.PlayOneShot(shootSE2);
                    if(i==2)
                    {
                        Instantiate(gob, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

                    }
                    else
                        Instantiate(stone, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(1.5f);

                }
            }

            if(enemy.hp<enemy.maxHP/8)
            {
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("ドガアアアアアアアアッー！");
                spaceship.GetAnimator().SetTrigger("Skill");
                for(;;)
                {
                    audioSource.PlayOneShot(shootSE2);
                    audioSource.PlayOneShot(shootSE);
                    ShotThrowAim(s2, pt, power, speed, BulletManager.BulletType.RockBullet);
                    audioSource.PlayOneShot(shootSE2);
                    common.ShotStoneFall(s2, speed, power, BulletManager.BulletType.RockBullet);
                    float xoffset = 1.4f;
                    float yoffset = Random.Range(-1.5f, 1.5f);
                    audioSource.PlayOneShot(shootSE2);
                    Instantiate(stone, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(1.5f);
                }
            }
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
