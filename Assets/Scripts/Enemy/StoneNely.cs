using UnityEngine;
using System.Collections;

public class StoneNely : MonoBehaviour {
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
			for(int i=0; i<5; ++i){
                audioSource.PlayOneShot(shootSE1);

				common.Shot(st,90,shotPower,2,BulletManager.BulletType.DarkChaser);
				yield return new WaitForSeconds(1.0f);
			}
			yield return new WaitForSeconds(7.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
