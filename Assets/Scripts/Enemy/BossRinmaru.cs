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
    public AudioClip shootSE1;
    public AudioClip shootSE2;
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

        common.ShowWindowMessage("剣士「追手か！」");
        yield return new WaitForSeconds(2.0f);
		common.ShowWindowMessage("剣士「メデュ様を連れて行かせるわけにはいかない！」");
        //yield return new WaitForSeconds(3.0f);

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
			for(int i=0; i<3; ++i){//通常攻撃
				Vector3 aimpos = new Vector3(Random.Range(0.5f,3.0f),Random.Range (-1.5f,1.5f),0);
				yield return StartCoroutine(enemy.MoveByTime(aimpos,0.7f));
				for(int j=0; j<2; ++j){
                    audioSource.PlayOneShot(shootSE1);
					common.ShotNWay(s2,90,power,shotSpeed,BulletManager.BulletType.SlashBullet,3,6);
					yield return new WaitForSeconds(0.1f);
				}
				yield return new WaitForSeconds(0.5f);
			}

            if (enemy.hp < enemy.maxHP/3)
            {//連続切り
				common.SetFlicker();
                audioSource.PlayOneShot(skillSE);
				common.ShowWindowMessage("連続切り");
				yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,0.6f,0),1.0f));
				yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 8; ++i)
                {
					yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,Random.Range (1.0f,2.5f),0),0.2f));
                    audioSource.PlayOneShot(shootSE2);
                    common.Shot(s2,90,10,8,BulletManager.BulletType.GrandSlash);
					yield return new WaitForSeconds(0.3f);

                    yield return StartCoroutine(enemy.MoveByTime(new Vector3(3.5f,Random.Range (-1.2f,0.2f),0),0.2f));
                    audioSource.PlayOneShot(shootSE2);
                    common.Shot(s2,90,10,8,BulletManager.BulletType.GrandSlash);
					yield return new WaitForSeconds(0.3f);
				}

			}
			else{
				common.SetFlicker();//特技時の点滅
                audioSource.PlayOneShot(skillSE);
				common.ShowWindowMessage("居合切り");
				yield return new WaitForSeconds(1.5f);
				yield return StartCoroutine(enemy.MoveByTime(pt.position + new Vector3(1f,0,0),0.2f));
				yield return new WaitForSeconds(0.5f);
                audioSource.PlayOneShot(shootSE2);
				
                common.Shot(s2,80,power*2,shotSpeed,BulletManager.BulletType.GrandSlash);
                common.Shot(s2,100,power*2,shotSpeed,BulletManager.BulletType.GrandSlash);
                /*
                for(int j=0; j<5; ++j){
					common.ShotNWay(s2,90,power,shotSpeed,BulletManager.BulletType.SlashBullet,2,8);
				
                  yield return new WaitForSeconds(0.1f);
				}*/
                yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
