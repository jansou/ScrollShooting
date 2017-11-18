using UnityEngine;
using System.Collections;

public class LastDragon : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform st2;
	Transform pt;

	public GameObject sun;
	public GameObject horizon;

    //SE関係
    public AudioClip shootSE1;
    public AudioClip shootSE2;
    public AudioClip shootSE3;
    public AudioClip shootSE4;

    public AudioClip skillSE;
    AudioSource audioSource;
    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>

	int maxHP;
	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = skillSE;


		st = common.CreateShotPosition();
		st2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		//弾が口から出るように
		st.position = transform.position + new Vector3(-1.4f,-0.2f,0);

		maxHP = enemy.hp;
		
		yield return new WaitForEndOfFrame();
		
		yield return StartCoroutine("Stop");


		//本番用

		yield return StartCoroutine(Attack(maxHP*8/10));
		yield return StartCoroutine("PrometeusFrame");
		yield return StartCoroutine(Attack(maxHP*6/10));
		yield return StartCoroutine("PhenomenonHorizon");
        yield return StartCoroutine(Attack(maxHP * 4 / 10));
        yield return StartCoroutine("PrometeusFrame");
        yield return StartCoroutine(Attack(maxHP * 3 / 10));
        yield return StartCoroutine("PhenomenonHorizon");

		yield return StartCoroutine(Attack(maxHP*2/10));
		yield return StartCoroutine("StardustJourney");

	}

	IEnumerator PrometeusFrame(){
        
        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
		common.ShowWindowMessage("プロメテウスの火");

        audioSource.PlayOneShot(shootSE1);
		GameObject g = (GameObject)Instantiate(sun,st.position,Quaternion.identity);
		while(g){
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator GalaxyGate(){
        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
		common.ShowWindowMessage("タンホイザーゲート");

		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator PhenomenonHorizon(){
        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
		common.ShowWindowMessage("事象の地平線");
        audioSource.PlayOneShot(shootSE2);

		GameObject g = (GameObject)Instantiate(horizon,st.position+new Vector3(-0.3f,0,0),Quaternion.identity);
		while(g){
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator StardustJourney(){
		st.position += new Vector3(1.5f,-0.5f,0);
        spaceship.GetAnimator().SetTrigger("Skill");
        audioSource.PlayOneShot(skillSE);
		common.ShowWindowMessage("ミルキーウェイ");
		int range = 0;

		StartCoroutine("RayRush");
		StartCoroutine("ScatterStar");

		while(true){
			common.Shot(st,range,10,2,BulletManager.BulletType.StarChaser);
			range += 20;
			yield return new WaitForSeconds(0.4f);
		}
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator ScatterStar(){
		while(true){
            audioSource.PlayOneShot(shootSE3);

			common.Shot(st,Random.Range (45,135),3,3,BulletManager.BulletType.StarBullet,0,0);
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator RayRush(){
		while(true){
            audioSource.PlayOneShot(shootSE4);

			st2.position = new Vector3(Random.Range(2,5),4,0);
			common.Shot(st2,Random.Range (115,155),10,7,BulletManager.BulletType.RayBullet,0,0);
			yield return new WaitForSeconds(1.3f);
		}
	}

	IEnumerator Attack(int breakHP){
		while(true){
			if(enemy.hp < breakHP){
				yield break;
			}
			yield return StartCoroutine("FireBress");
			yield return new WaitForSeconds(1.0f);
			if(enemy.hp < breakHP){
				yield break;
			}
			yield return StartCoroutine("DarknessCore");
			yield return new WaitForSeconds(1.0f);
			if(enemy.hp < breakHP){
				yield break;
			}
			yield return StartCoroutine("DarkChaser");
			yield return new WaitForSeconds(1.0f);
			if(enemy.hp < breakHP){
				yield break;
			}
		}
	}

	IEnumerator Stop(){
		FindObjectOfType<MessageWindow>().showMessage("冥王竜メナス=プルートゥ");
		yield return new WaitForSeconds(4);
		enemy.MoveStop();
	}


	IEnumerator DarkChaser(){
		for(int i=0; i<8; ++i){
            audioSource.PlayOneShot(shootSE3);
			common.Shot(st,45*i,5,3,BulletManager.BulletType.DarkChaser,0,0);
			yield return new WaitForSeconds(0.3f);
		}
	}

	IEnumerator DarknessCore(){
		for(int i=0; i<3; ++i){
            audioSource.PlayOneShot(shootSE2);
			common.ShotAim(st,pt,20,3,BulletManager.BulletType.DarknessCore);
			yield return new WaitForSeconds(1.5f);
		}
	}

	//火を吐く（１回分)
	IEnumerator FireBress(){
		for(int i=0; i<40; ++i){
            audioSource.PlayOneShot(shootSE1);
			common.ShotAim(st,pt,3,Random.Range(4,7),BulletManager.BulletType.BlossomBullet,Random.Range(-3,3));
			common.ShotAim(st,pt,3,Random.Range(2,4),BulletManager.BulletType.BlossomBullet,Random.Range(-10,10));
			
			if(i % 10 == 0){
				common.ShotAim(st,pt,3,Random.Range(2,7),BulletManager.BulletType.FireStop,Random.Range(-5,5));
			}
			
			yield return new WaitForSeconds(0.03f);
		}	
		yield return new WaitForSeconds(1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
