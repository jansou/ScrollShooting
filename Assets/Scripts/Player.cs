﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour 
{
	// 移動スピード
	public float speed = 5;

    public int level = 1;

    public int hp = 10;

    public int shotPower=1;

    public int exp=0;

	//public GameObject bullet;

	//Spaceshipコンポーネント
	Spaceship spaceship;

    //Backgroundコンポーネント
    Background background;

	Transform shotposition;

	IEnumerator Start()
	{
        hp *= level;

		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        background = FindObjectOfType<Background>();

		shotposition = transform.GetChild(0);

		while (true) 
		{
			spaceship.Shot(shotposition,shotPower);
			//Instantiate(bullet, transform.position,transform.rotation);

			GetComponent<AudioSource>().Play();

			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}


	void Update ()
	{
		// 右・左
		//float x = Input.GetAxisRaw ("Horizontal");
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

		// 上・下
		//float y = Input.GetAxisRaw ("Vertical");
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");

		// 移動する向きを求める
		//Vector2 direction = new Vector2 (x, y).normalized;
        Vector2 direction = new Vector2(x, y);
		Move (direction);

		// 移動する向きとスピードを代入する
		//GetComponent<Rigidbody2D>().velocity = direction * speed;
		//spaceship.Move (direction);

		//移動の制限(だめな例)
		//Clamp ();
	}

	void Move(Vector3 direction)
	{
        //背景のスケール
        Vector2 scale = background.transform.localScale;
        /*
		//画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		//画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        */

        Vector2 min = scale * -0.5f;

        Vector2 max = scale * 0.5f;
		//プレイヤー座標の取得
		Vector3 pos = transform.position;

		//移動量を加える
		pos += direction * spaceship.speed * Time.deltaTime;

		//プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}

	/*//画面外に一瞬はみ出してしまうため、よくない
	void Clamp()
	{
		//画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		//画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		//プレイヤーの座標を取得
		Vector2 pos = transform.position;

		//プレイヤーの位置が画面内になるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}
	*/


	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D (Collider2D c)
	{
		//レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

        /*
		//レイヤー名がBullet(Enemy)の時は弾を削除
		if (layerName == "Bullet(Enemy)") 
		{
			// 弾の削除
			Destroy(c.gameObject);
		}
         */

		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") 
		{
            Transform enemyBulletTransform = c.transform;

            Bullet bullet = enemyBulletTransform.GetComponent<Bullet>();

            hp = hp - bullet.getDamage();//bullet.power;

            if (layerName == "Bullet(Enemy)")
            {
                // 弾の削除
                Destroy(c.gameObject);
            }

            if (hp <= 0)
            {
                //Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
                FindObjectOfType<Manager>().GameOver();

                //爆発する
                spaceship.Explosion();

                //プレイヤー削除
                Destroy(gameObject);
            }
            else
            {
                spaceship.GetAnimator().SetTrigger("Damage");
            }
		}

	}

    public void addExp(int point)
    {
        exp += point;
    }

}