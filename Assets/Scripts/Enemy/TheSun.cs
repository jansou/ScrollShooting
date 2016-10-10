using UnityEngine;
using System.Collections;

public class TheSun : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;

	float normalScale = 0.5f;
	float time = 0.0f;

    //SE関係
    public AudioClip shootSE1;
    AudioSource audioSource;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();

		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		enemy.Move(new Vector2(-3,0));
		yield return new WaitForSeconds(2.0f);
		enemy.MoveStop();

		for(int i=0; i<100; ++i){
            audioSource.PlayOneShot(shootSE1);

			common.Shot(st,Random.Range(0,360),5,3,BulletManager.BulletType.BlossomBullet,0,0);
            common.Shot(st, Random.Range(0, 360), 5, 3, BulletManager.BulletType.BlossomBullet, 0, 0);
            common.Shot(st, Random.Range(0, 360), 5, 3, BulletManager.BulletType.BlossomBullet, 0, 0);
            yield return new WaitForSeconds(0.2f);
			normalScale += 0.5f/100;
		}
		enemy.ExplodeSelf();
	}

	void OnDestroy(){
		int n = (int)(transform.localScale.x * 100);
		for(int i=0; i<n; ++i){
			float dig = Random.Range(0.0f,6.28f);
			Vector2 dir = new Vector2(Mathf.Cos(dig),Mathf.Sin(dig)) * Random.Range(0,2);
			int rad = (int)(360 * dig / 6.28f);
			common.Shot(st,rad,3,Random.Range(1,4),BulletManager.BulletType.BlossomBullet,0,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float s = normalScale + 0.05f * Mathf.Sin(time * 4);
		transform.localScale = new Vector3(s,s,s);
		time += Time.deltaTime;
	}
}
