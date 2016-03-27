using UnityEngine;
using System.Collections;

public class BossDragon : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;

	Transform st;
	Transform pt;


	// Use this for initialization
	IEnumerator Start (){
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();
		
		yield return StartCoroutine("Stop");

		StartCoroutine("Attack");
		//StartCoroutine("TestA");
	}

	IEnumerator Stop(){
		FindObjectOfType<MessageWindow>().showMessage("スカイドラゴン");
		yield return new WaitForSeconds(2);
		enemy.MoveStop();
	}
	IEnumerator TestA(){
		yield return null;

	}

	IEnumerator WaveShot(){
		int r = Random.Range(0,2);
		if(r==0){
			for(int i=0; i<30; ++i){
				common.Shot(st,150-2*i,5,10,BulletManager.BulletType.SlashBullet);
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(0.5f);
			for(int i=0; i<30; ++i){
				common.Shot(st,30+2*i,5,10,BulletManager.BulletType.SlashBullet);
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(0.5f);
		}
		else{
			for(int i=0; i<30; ++i){
				common.Shot(st,30+2*i,5,10,BulletManager.BulletType.SlashBullet);
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(0.5f);
			for(int i=0; i<30; ++i){
				common.Shot(st,150-2*i,5,10,BulletManager.BulletType.SlashBullet);
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator Attack(){
		while(true){
			for(int i=0; i<2; ++i){
				st.position = transform.position;

				yield return StartCoroutine("WaveShot");

				yield return StartCoroutine(enemy.MoveByTime(new Vector3(4.0f,pt.position.y,0),1.5f));

				enemy.Move(Vector3.left*20);

				while(transform.position.x > -4.0f){
					common.Shot(st,Random.Range(-45,45),1,2,BulletManager.BulletType.SlashBullet);
					common.Shot(st,180+Random.Range(-45,45),1,2,BulletManager.BulletType.SlashBullet);
					yield return new WaitForSeconds(0.08f);
				}
				enemy.MoveStop();
				yield return new WaitForSeconds(0.5f);
				yield return StartCoroutine(enemy.MoveByTime(new Vector3(pt.position.x,3.0f,0),1.5f));

				enemy.Move (Vector3.down*15);

				while(transform.position.y > -3.0f){
					common.Shot(st,90+Random.Range(-45,45),1,2,BulletManager.BulletType.SlashBullet);
					common.Shot(st,270+Random.Range(-45,45),1,2,BulletManager.BulletType.SlashBullet);
					yield return new WaitForSeconds(0.08f);
				}
				enemy.MoveStop();
				yield return new WaitForSeconds(0.5f);
				yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.0f,1.0f,0),1.5f));
			}

			common.SetFlicker();
			common.ShowWindowMessage("大きく息を吸っている...");
			yield return new WaitForSeconds(3.0f);
			common.ShowWindowMessage("ファイアブレス");
			
			st.position = transform.position + new Vector3(0,-0.6f,0);
			for(int j=0; j<3; ++j){
				for(int i=0; i<50; ++i){
					common.ShotAim(st,pt,3,Random.Range(5,8),BulletManager.BulletType.BlossomBullet,Random.Range(-3,3));
					common.ShotAim(st,pt,3,Random.Range(3,5),BulletManager.BulletType.BlossomBullet,Random.Range(-10,10));
					
					if(i % 10 == 0){
						common.ShotAim(st,pt,3,Random.Range(3,8),BulletManager.BulletType.FireStop,Random.Range(-5,5));
					}
					
					yield return new WaitForSeconds(0.03f);
				}
				yield return new WaitForSeconds(1.5f);
			}
			yield return new WaitForSeconds(1.0f);
		}

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
