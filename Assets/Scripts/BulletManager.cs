using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	public GameObject bullet;
	public GameObject slashBullet;
	public GameObject playerBullet;
	public GameObject playerSlashBullet;
	public GameObject playerHomingBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public enum BulletType{
		Normal,
		Slash,
		Player,
		PSlash,
		PHoming
	};

	public void Shot(Transform origin, int shotPower,int shotSpeed=2, BulletType type = BulletType.Normal)
	{
		GameObject b = bullet;
		switch(type){
		case BulletType.Normal:
			b = bullet;
			break;
		case BulletType.Slash:
			b = slashBullet;
			break;
		case BulletType.Player:
			b = playerBullet;
			break;
		case BulletType.PSlash:
			b = playerSlashBullet;
			break;
		case BulletType.PHoming:
			b = playerHomingBullet;
			break;
		}

		GameObject a = (GameObject)Instantiate(b,origin.position,origin.rotation);
		a.GetComponent<Bullet>().shotPower = shotPower;
		a.GetComponent<Bullet>().speed = shotSpeed;
	}
}
