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

	public void ShotNWay(Transform origin, int rotate, int shotPower, int shotSpeed=2, BulletManager.BulletType type = BulletManager.BulletType.Normal,int way = 1,int spread = 0,float offsetx=0,float offsety=0){
		for(int i=0; i<way; ++i){
			int r = rotate + spread * (i - (way/2));
			if(way % 2 == 0){
				r += spread /2;
			}
			Shot (origin,r,shotPower,shotSpeed,type,offsetx,offsety);
		}
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
	public void ShotAimNway(Transform origin,Transform aim, int shotPower,int shotSpeed, BulletManager.BulletType type,int way=1, float spread=0){
		if(aim){
			Vector3 v = aim.position - transform.position;
			Quaternion q = Quaternion.FromToRotation(Vector3.up,v);
			 
			for(int i=0; i<way; ++i){
				float d = spread * (i-(way/2));
				origin.localRotation = Quaternion.Euler (0,0,q.eulerAngles.z+d);
				spaceship.Shot(origin,shotPower,shotSpeed,type);
			}
		}
		else{
			Shot (origin,90,shotPower,shotSpeed,type);
		}
	}

	//メッセージウィンドウに表示
	public void ShowWindowMessage(string str){
		FindObjectOfType<MessageWindow>().showMessage(str);
	}

	//スキル発動用のチカチカ
	public void SetFlicker(){
		spaceship.GetAnimator().SetTrigger("Skill");
	}

    public void ShotStoneFall(Transform origin, int shotPower, int shotSpeed, BulletManager.BulletType type)
    {
        for(int i=0;i<5;++i)
        {
            origin.position = new Vector3(Random.Range(-3.0f, 3.0f), i, origin.localPosition.z);
            spaceship.Shot(origin, shotPower, 0, type);
        }
    }


	
	// Update is called once per frame
	void Update () {
	
	}
}
