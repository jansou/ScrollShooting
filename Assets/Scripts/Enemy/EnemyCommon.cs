using UnityEngine;
using System.Collections;

public class EnemyCommon : MonoBehaviour {

	Transform shotPositions;

	Spaceship spaceship;

	// Use this for initialization
	void Start () {
	}

	public void Init(){
		GameObject oo = new GameObject();
		oo.name = "shotPositions";
		oo.transform.parent = transform;
		oo.transform.localPosition = new Vector3(0,0,0);
		shotPositions = oo.transform;

		spaceship = GetComponent<Spaceship>();
	}

	public Transform CreateShotPosition(){
		GameObject o = (GameObject)Instantiate(Resources.Load("ShotPosition"));
		o.transform.parent = shotPositions;
		o.transform.localPosition = new Vector3(0,0,0);
		return o.transform;
	}

	public void Shot(Transform origin, int rotate, int shotPower, int shotSpeed=2, BulletManager.BulletType type = BulletManager.BulletType.Normal,float offsetx=0,float offsety=0){
		origin.localRotation = Quaternion.Euler(0,0,rotate);
		spaceship.Shot(origin,shotPower,shotSpeed,type,offsetx,offsety);
	}
	public void ShotAim(Transform origin,Transform aim, int shotPower,int shotSpeed, BulletManager.BulletType type){
		if(aim){
			Vector3 v = aim.position - transform.position;
			origin.localRotation = Quaternion.FromToRotation(Vector3.up,v);
			spaceship.Shot(origin,shotPower,shotSpeed,type);
		}
		else{
			Shot (origin,90,shotPower,shotSpeed,type);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
