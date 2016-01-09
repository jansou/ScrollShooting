using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{

	//弾のスピード
	public int speed = 10;
	// Use this for initialization

	//ゲームオブジェクト生成から削除するまでの時間
	public float liftTime=5;

    public int shotPower = 1;

    //ダメージ倍率
    public float magnification = 1;

	void Start () 
	{
		//ローカル座標のY軸方向に移動する
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	
		//liftTime秒後に削除
		Destroy (gameObject, liftTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getDamage()
    {
        return Mathf.RoundToInt(shotPower * magnification);
    }
}
