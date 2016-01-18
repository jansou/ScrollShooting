﻿using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour 
{
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 1;
    public int speed = 3;
    Enemy enemy;

    Transform s1;
    Transform pt;

	IEnumerator Start () 
    {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

		s1 = common.CreateShotPosition();
        pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");
        StartCoroutine("Attack1");

        yield break;
	}

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

    IEnumerator Attack1()
    {

        while (true)
        {
            common.ShotAim(s1, pt, power, speed, BulletManager.BulletType.BananaSlash);

            yield return new WaitForSeconds(spaceship.shotDelay);
        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}