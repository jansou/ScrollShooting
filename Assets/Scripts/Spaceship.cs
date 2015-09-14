using UnityEngine;
using System.Collections;

//Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Spaceship : MonoBehaviour 
{
	//移動スピード
	public float speed=0f;

	//弾を撃つ間隔
	public float shotDelay;

	//弾のPrefab
	public GameObject bullet;

	//弾撃てるかどうか
	public bool canShot;

	//爆発のprefab
	public GameObject explosion;

    //アニメーターコンポーネント
    private Animator animator;

    void Start()
    {
        //アニメコンポーネントを取得
        animator = GetComponent<Animator>();
    }

	//爆発の作成
	public void Explosion()
	{
		Instantiate(explosion, transform.position,transform.rotation);
	}

	//弾の作成
	public void Shot(Transform origin)
	{
		Instantiate(bullet,origin.position,origin.rotation);
	}

    public Animator GetAnimator()
    {
        return animator;
    }

	/*
	//機体の移動
	public void Move(Vector2 direction)
	{
		//GetComponent<Rigidbody2D>().velocity = direction * speed;
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	*/
}
