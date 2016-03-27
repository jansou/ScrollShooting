using UnityEngine;
using UnityEngine;
using System.Collections;

public class BossDeathAngel : MonoBehaviour {
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

	public GameObject summonZonbie;
	public GameObject summonZonbieWall;


	
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
		
		FindObjectOfType<MessageWindow>().showMessage("マミエル");
		
		yield return StartCoroutine("Stop");
		
		StartCoroutine ("TestAttack");
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
		//common.ShowWindowMessage("召喚");
		GameObject o = (GameObject) Instantiate(summonZonbieWall,new Vector3(-5.7f,0.3f,0),Quaternion.identity);

		StartCoroutine("MoveUpDown");

		StartCoroutine("Attack1");

		while(true){
			for(int i=0; i<10; ++i){
				Vector3 rp = transform.position + new Vector3(Random.Range(-2.0f,1.0f),Random.Range(-2.0f,2.0f),0);
				GameObject z = (GameObject)Instantiate(summonZonbie,rp,Quaternion.identity);
				yield return new WaitForSeconds(1.0f);
			}
			spaceship.GetAnimator().SetTrigger("Skill");
			for(int i=0; i<10; ++i){
				s2.position = new Vector3(Random.Range(-3.0f,2.0f),4.0f,0);
				common.Shot(s2,180,5,0,BulletManager.BulletType.BoneBullet,0,0);
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(2.0f);
		}
	}

	IEnumerator MoveUpDown(){
		float moveFrame = 0;

		while(true){
			Vector2 mv;
			mv.x = 0;
			mv.y = 4.5f * Mathf.Cos(0.02f * moveFrame);
			enemy.Move(mv);
			if(Time.timeScale != 0){
				moveFrame += 60 * Time.deltaTime;
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	IEnumerator Attack1()
	{
		while(true){
			s2.position = transform.position;
			common.ShotNWay(s2,90,4,4,BulletManager.BulletType.SlashBullet,3,25);
			yield return new WaitForSeconds(2.5f);
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
