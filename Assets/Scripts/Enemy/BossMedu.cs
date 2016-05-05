using UnityEngine;
using System.Collections;

public class BossMedu : MonoBehaviour {
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

    //召喚するやつ
	public GameObject summonSlime;
	public GameObject summonMonkey;
	public GameObject summonBlossom;
	public GameObject summonBossMonkey;
	public GameObject summonForestSpirit;

	public GameObject summonRecoveryField;

	int maxHP = 0;
	
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

		maxHP = enemy.hp;

		yield return new WaitForEndOfFrame();

        FindObjectOfType<MessageWindow>().showMessage("メデュ");

        StartCoroutine("Stop");
        yield return new WaitForSeconds(3.0f);
		StartCoroutine("Attack1");
		//StartCoroutine("Attack2");

		//StartCoroutine ("TestAttack");
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

	IEnumerator TestAttack()
	{
		while(true){
			s2.position = new Vector3(Random.Range(-3.0f,0f),6.0f,0.0f);
			common.Shot(s2,180,5,10,BulletManager.BulletType.RayBullet);
			yield return new WaitForSeconds(1.0f);
		}
	}

	IEnumerator Attack1()
    {//
		spaceship.GetAnimator().SetTrigger("Skill");
		audioSource.PlayOneShot(skillSE);
		FindObjectOfType<MessageWindow>().showMessage("少女「リンマルにいじわるしないで！」");
		while(enemy.hp > maxHP*4/5){
			for(int i=0; i<2; ++i){
				Vector3 c = transform.position;
				float rx = c.x-0.2f;
				float ry = Random.Range(c.y-1.5f,c.y+1.5f);
	            Instantiate(summonSlime, new Vector3(rx,ry,0), Quaternion.identity);
			}
	        yield return new WaitForSeconds(3.5f);	        
		}

		spaceship.GetAnimator().SetTrigger("Skill");
		audioSource.PlayOneShot(skillSE);
		FindObjectOfType<MessageWindow>().showMessage("少女「やめてったら！」");
		while(enemy.hp > maxHP*3/5){
			Vector3 c = transform.position;
			Instantiate(summonBlossom,c+new Vector3(-0.5f,0.8f,0),Quaternion.identity);
			Instantiate(summonBlossom,c+new Vector3(-0.5f,-0.8f,0),Quaternion.identity);
			//Instantiate(summonBlossom,c+new Vector3(0.5f,1.5f,0),Quaternion.identity);
			//Instantiate(summonBlossom,c+new Vector3(0.5f,-1.5f,0),Quaternion.identity);
			yield return new WaitForSeconds(4.5f);
		}

        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("少女「本気でおこったんだから！」");
        yield return new WaitForSeconds(2.0f);

        //以下ループ行動
		while (true) 
		{
			if (!GameObject.Find("EnemyBossForestSpiritR(Clone)"))
			{
				spaceship.GetAnimator().SetTrigger("Skill");
				audioSource.PlayOneShot(skillSE);
				FindObjectOfType<MessageWindow>().showMessage("少女「命の力よ！」");
			
				//Instantiate(armor, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
				Instantiate(summonForestSpirit,transform.position+new Vector3(-0.5f,0,0) , Quaternion.identity);
                Instantiate(summonRecoveryField, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                //Instantiate(summonBlossom, c + new Vector3(-0.5f, -0.8f, 0), Quaternion.identity);

                //Instantiate(armor, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
				yield return new WaitForSeconds(5.0f);
			}
            else if(count == 2)
            {
               // while(true)
                {
                    spaceship.GetAnimator().SetTrigger("Skill");
                    audioSource.PlayOneShot(skillSE);
                    FindObjectOfType<MessageWindow>().showMessage("チャージ");

                    yield return new WaitForSeconds(4.0f);

                    spaceship.GetAnimator().SetTrigger("Skill");
                    audioSource.PlayOneShot(skillSE);
                    FindObjectOfType<MessageWindow>().showMessage("レイ");

					for(int i=0; i<5; ++i){
						audioSource.PlayOneShot(shootSE);
						s2.position = new Vector3(Random.Range(-3.0f,0f),6.0f,0.0f);
						common.Shot(s2,180,5,10,BulletManager.BulletType.RayBullet);
						yield return new WaitForSeconds(1.0f);
					}
                    count = 0;

                    yield return new WaitForSeconds(2.0f);
                }
            }
                /*
            else if (!GameObject.Find("EnemyBossMonkeyR(Clone)"))
            {
                spaceship.GetAnimator().SetTrigger("Skill");
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("巨大猿召喚");

                {
                    float xoffset = 1.4f;
                    float yoffset = 1.0f;
                    Instantiate(summonBossMonkey, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
                    //Instantiate(soldier, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
                    //Instantiate(summonBossMonkey, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
				}
				yield return new WaitForSeconds(3.0f);
            }*/
            else
            {
				/*
                spaceship.GetAnimator().SetTrigger("Skill");
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("猿召喚");

                for (int n = 0; n < 5; ++n)
                {
                    float xoffset = 1.4f;
                    float yoffset = 1.0f;
                    Instantiate(summonMonkey, new Vector3(transform.position.x + xoffset, transform.position.y - yoffset, transform.position.z), Quaternion.identity);
					Instantiate(summonMonkey, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z), Quaternion.identity);
					Instantiate(summonMonkey, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);

                    //common.ShotAim(s2, pt, power, shotSpeed, BulletManager.BulletType.BananaSlash);

                    //shotDelay秒待つ
                    yield return new WaitForSeconds(1.5f);
                }
                */
				//spaceship.GetAnimator().SetTrigger("Skill");
				//FindObjectOfType<MessageWindow>().showMessage("リカバリーフィ－ルド");
				//GameObject o = (GameObject)Instantiate(summonRecoveryField,transform.position,Quaternion.identity);
				//o.GetComponent<Enemy>().Move(new Vector2(Random.Range(-2.0f,2.0f),Random.Range(-2.0f,2.0f)));

				yield return new WaitForSeconds(5.0f);

                ++count;
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
