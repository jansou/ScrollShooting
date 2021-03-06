﻿using UnityEngine;
using System.Collections;

public class TheHorizon : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;

	Transform st;
	Transform st2;

	Transform pt;

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
		st.position = st.position + new Vector3(0,-1.7f,0);
		st2 = common.CreateShotPosition();
		st2.position = st2.position + new Vector3(0,1.7f,0);

		pt = FindObjectOfType<Party>().transform;


		StartCoroutine("MoveStop");
		StartCoroutine("Magne");
		StartCoroutine("Move");
		int span = 0;
		while(true)
		{
            audioSource.PlayOneShot(shootSE1);
			if(span % 5 == 0){
				int range = 50;
				common.Shot(st,Random.Range(-range,range),3,2,BulletManager.BulletType.StarBullet);
				common.Shot(st2,180+Random.Range(-range,range),3,2,BulletManager.BulletType.StarBullet);
			}
			else{
				int range = 5;
				common.Shot(st,Random.Range(-range,range),2,Random.Range(3,6),BulletManager.BulletType.StarBullet);
				common.Shot(st2,180+Random.Range(-range,range),2,Random.Range(3,6),BulletManager.BulletType.StarBullet);
			}
			++span;
			yield return new WaitForSeconds(0.05f);
		}

		yield return null;
	}

	IEnumerator MoveStop(){
		while(transform.position.x > 0.0f){
			yield return new WaitForEndOfFrame();
		}
		enemy.MoveStop();
	}

	IEnumerator Move(){
		float d=0;
		while(true){
			transform.rotation = Quaternion.Euler(0,0,90 * Mathf.Cos(d));
			d += 0.5f * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}


	IEnumerator Magne(){
		while(true){
			Vector3 dir = transform.position - pt.position;
			pt.transform.Translate(dir.normalized * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
