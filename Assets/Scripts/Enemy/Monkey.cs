using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 2;
    //SE関係
    public AudioClip shootSE;
    AudioSource shootSESource;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

        shootSESource = gameObject.GetComponent<AudioSource>();
        shootSESource.clip = shootSE;

		Transform s1 = common.CreateShotPosition();

		yield return new WaitForEndOfFrame();
		while (true) 
		{
			common.Shot(s1,60+Random.Range(0,60),power,2,BulletManager.BulletType.BananaSlash);
            shootSESource.PlayOneShot(shootSE);

			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
