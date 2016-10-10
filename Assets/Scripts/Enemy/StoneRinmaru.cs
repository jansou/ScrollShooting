using UnityEngine;
using System.Collections;

public class StoneRinmaru : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;
    public int shotPower = 5;

    public AudioClip shootSE1;

    AudioSource audioSource;

	// Use this for initialization
	IEnumerator  Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();
		
		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

        audioSource = gameObject.GetComponent<AudioSource>();


		yield return new WaitForSeconds(2.0f);
		
		StartCoroutine("Attack");
	}
	
	IEnumerator Attack(){
		while(true){
			yield return new WaitForSeconds(4.0f);
			for(int i=0; i<20; ++i){
                audioSource.PlayOneShot(shootSE1);

				common.ShotNWay(st,90+i*10,shotPower,7,BulletManager.BulletType.SlashBullet,4,90);
				yield return new WaitForSeconds(0.2f);
			}
			yield return new WaitForSeconds(9.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
