using UnityEngine;
using System.Collections;

public class LastDragon : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;

	public GameObject sun;
	
	int maxHP;
	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();
		
		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		//弾が口から出るように
		st.position = transform.position + new Vector3(-1.4f,-0.2f,0);

		maxHP = enemy.hp;
		
		yield return new WaitForEndOfFrame();
		
		yield return StartCoroutine("Stop");

		yield return StartCoroutine(Attack(maxHP*4/5));
		yield return StartCoroutine("PrometeusFrame");
		yield return StartCoroutine(Attack(maxHP*3/5));
		yield return StartCoroutine("GalaxyGate");
		yield return StartCoroutine(Attack(maxHP*2/5));
		yield return StartCoroutine("PhenomenonHorizon");
		yield return StartCoroutine(Attack(maxHP*1/5));
		yield return StartCoroutine("StardustJourney");
	}

	IEnumerator PrometeusFrame(){
		common.ShowWindowMessage("プロメテウスの火");
		GameObject g = (GameObject)Instantiate(sun,st.position,Quaternion.identity);
		while(g){
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator GalaxyGate(){
		common.ShowWindowMessage("タンホイザーゲート");
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator PhenomenonHorizon(){
		common.ShowWindowMessage("事象の地平線");
		yield return new WaitForSeconds(4.0f);
	}
	IEnumerator StardustJourney(){
		common.ShowWindowMessage("スターダストジャーニー");
		yield return new WaitForSeconds(4.0f);
	}

	IEnumerator Attack(int breakHP){
		while(true){
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

	IEnumerator Magne(){
		while(true){
		float time = 0.0f;
		while(time < 5.0f){
			pt.transform.Translate(1*Time.deltaTime,0,0);
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
			yield return new WaitForSeconds(3.0f);
		}
	}

	IEnumerator DarkChaser(){
		for(int i=0; i<8; ++i){
			common.Shot(st,45*i,5,3,BulletManager.BulletType.DarkChaser,0,0);
			yield return new WaitForSeconds(0.3f);
		}
	}

	IEnumerator DarknessCore(){
		for(int i=0; i<3; ++i){
			common.ShotAim(st,pt,20,3,BulletManager.BulletType.DarknessCore);
			yield return new WaitForSeconds(1.5f);
		}
	}

	//火を吐く（１回分)
	IEnumerator FireBress(){
		for(int i=0; i<40; ++i){
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
