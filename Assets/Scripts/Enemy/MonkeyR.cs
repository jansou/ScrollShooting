using UnityEngine;
using System.Collections;

public class MonkeyR : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    public int power = 2;

    Transform s1;
    Transform pt;

    //SE関係
    public AudioClip shootSE;
    AudioSource shootSESource;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		common.Init();

        shootSESource = gameObject.GetComponent<AudioSource>();
        //shootSESource.clip = shootSE;

        s1 = common.CreateShotPosition();
        pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();
		while (true) 
		{
			//common.Shot(s1,60+Random.Range(0,60),power,2,BulletManager.BulletType.BananaSlash);
            common.ShotAim(s1, pt, power, 3, BulletManager.BulletType.BananaSlash);
                
            shootSESource.PlayOneShot(shootSE);

			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
