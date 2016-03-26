using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	public GameObject bullet;
	public GameObject slashBullet;
	public GameObject playerBullet;
	public GameObject playerSlashBullet;
	public GameObject playerHomingBullet;
	public GameObject playerGuylusBullet;
    public GameObject bananaSlash;
    public GameObject slimeBullet;
    public GameObject blossomBullet;
	public GameObject circleLeafBullet;
    public GameObject leafBullet;
    public GameObject allowBullet;
    public GameObject grandSlash;
    public GameObject darknessCore;
	public GameObject darkChaser;
    public GameObject rockBullet;
	public GameObject rayBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public enum BulletType{
		Normal,
		SlashBullet,
		Player,
		PSlash,
		PHoming,
		PGuylus,
        BananaSlash,
        SlimeBullet,
        BlossomBullet,
		CircleLeaf,
        LeafBullet,
        AllowBullet,
        GrandSlash,
        DarknessCore,
		DarkChaser,
        RockBullet,
		RayBullet
	};

	public void Shot(Transform origin, int shotPower,float shotSpeed=2, BulletType type = BulletType.Normal,float offsetx=0, float offsety=0)
	{
		GameObject b = bullet;
		switch(type){
		case BulletType.Normal:
			b = bullet;
			break;
        case BulletType.SlashBullet:
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
		case BulletType.PGuylus:
			b = playerGuylusBullet;
			break;
        case BulletType.BananaSlash:
            b = bananaSlash;
            break;
        case BulletType.SlimeBullet:
            b = slimeBullet;
            break;
        case BulletType.BlossomBullet:
            b = blossomBullet;
			break;
		case BulletType.CircleLeaf:
			b = circleLeafBullet;
            break;
        case BulletType.LeafBullet:
            b = leafBullet;
            break;
        case BulletType.AllowBullet:
            b = allowBullet;
            break;
        case BulletType.GrandSlash:
            b = grandSlash;
            break;
        case BulletType.DarknessCore:
            b = darknessCore;
            break;
		case BulletType.DarkChaser:
			b = darkChaser;
			break;
        case BulletType.RockBullet:
            b = rockBullet;
            break;
		case BulletType.RayBullet:
			b = rayBullet;
			break;
		}

		GameObject a = (GameObject)Instantiate(b,origin.position + new Vector3(offsetx,offsety),origin.rotation);
		a.GetComponent<Bullet>().shotPower = shotPower;
		a.GetComponent<Bullet>().speed = shotSpeed;
	}
}
