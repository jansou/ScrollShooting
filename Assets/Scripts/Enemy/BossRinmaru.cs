using UnityEngine;
using System.Collections;

public class BossRinmaru : MonoBehaviour {
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

		s2 = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		common.ShowWindowMessage("リンマル");

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
        yield return new WaitForSeconds(3.0f);
		StartCoroutine("Attack1");



		yield break;
	}

    IEnumerator Stop()
    {
    	//yield return new WaitForSeconds(1.0f);
        while (this.transform.position.x > 3.0f){
			yield return new WaitForSeconds(1.0f);
		}
        //audioSource.PlayOneShot(shootSE);
        enemy.MoveStop();
        //enemy.MoveAim(transform.position, pt.position, 4);
        yield return null;
    }

	IEnumerator Attack1()
    {
        while (true)
        {//enemy.MoveByTime(new Vector3(3.5f,Random.Range (1.0f,2.5f),0)場所,0.2f時間)
			for(int i=0; i<7; ++i){//通常攻撃
				Vector3 aimpos = new Vector3(Random.Range(0.5f,4.0f),Random.Range (-1.4f,3f),0);
				yield return StartCoroutine(enemy.MoveByTime(aimpos,0.7f));
				for(int j=0; j<2; ++j){
					common.ShotNWay(s2,90,3,6,BulletManager.BulletType.SlashBullet,2,6);
					yield return new WaitForSeconds(0.1f);
				}
				yield return new WaitForSeconds(0.5f);
			}

			if(enemy.hp < 150){//連続切り
				common.SetFlicker();
				common.ShowWindowMessage("連続切り");
				yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,0.6f,0),1.0f));
				yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 8; ++i)
                {
					yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,Random.Range (1.0f,2.5f),0),0.2f));
					common.Shot(s2,90,10,8,BulletManager.BulletType.GrandSlash);
					yield return new WaitForSeconds(0.3f);
					yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,Random.Range (-1.2f,0.2f),0),0.2f));
					common.Shot(s2,90,10,8,BulletManager.BulletType.GrandSlash);
					yield return new WaitForSeconds(0.3f);
				}

			}
			else{
				common.SetFlicker();//特技時の点滅
				common.ShowWindowMessage("居合切り");
				yield return new WaitForSeconds(1.5f);
				yield return StartCoroutine(enemy.MoveByTime(pt.position + new Vector3(1f,0,0),0.2f));
				yield return new WaitForSeconds(0.5f);
		        audioSource.PlayOneShot(skillSE);
				for(int j=0; j<5; ++j){
					common.ShotNWay(s2,90,3,6,BulletManager.BulletType.SlashBullet,2,8);
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
