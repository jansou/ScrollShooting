using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    //ヒットポイント
    public int hp = 1;

    // スコアのポイント
    public int point = 100;

    //接触時の攻撃力
    public int touchDamage = 1;

	//Spaceshipコンポーネント
	Spaceship spaceship;

    public int shotPower = 1;
	
	// Use this for initialization
	IEnumerator Start () 
	{
		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

		//ローカル座標のX軸のマイナス方向に移動する
		//spaceship.Move (transform.up * -1);

        //Debug.Log("right:" + transform.right + ", up:" + transform.up);
        Move(transform.right*-1);

		//canShotがfalseの場合、ここでコルーチンを終了させる
		if (spaceship.canShot == false) 
		{
			yield break;
		}		
	}

    //機体の移動
    public void Move(Vector2 direction)
    {
        //GetComponent<Rigidbody2D>().velocity = direction * speed;
        //GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
        //GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
        //Debug.Log("direction:" + direction + ", spaceship: " + spaceship.speed + "move:" + direction * spaceship.speed);
    }

	public void MoveAim(Vector3 from, Vector3 to, float speed){
		Vector3 v = to-from;
		Quaternion q = Quaternion.FromToRotation(Vector3.up,v);
		Move(q * Vector3.up * speed);
	}

    public void MoveStop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

	void OnTriggerEnter2D(Collider2D c)
	{
		//レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		//レイヤー名がBullet(Player)以外の時は何も行わない
		if (layerName != "Bullet(Player)")
			return;

        //PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;

        //Bulletコンポーネントを取得
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        //ヒットポイントを減らす
        hp = hp - bullet.getDamage();

		Vector3 range = new Vector3(Random.Range(-0.1f,0.1f),Random.Range(-0.1f,0.1f),0);
		FindObjectOfType<PopUp>().CreateText(transform.position+range,bullet.getDamage());

		//弾の削除
		Destroy (c.transform.parent.gameObject);

        //ヒットポイントが０以下で爆発
        if (hp <= 0)
        {
            // スコアコンポーネントを取得してポイントを追加
            FindObjectOfType<Score>().AddPoint(point);

            //爆発
            spaceship.Explosion();


            //エネミーの削除
            Destroy(gameObject);
        }
        else
        {
            if (spaceship != null)
            {
                //Debug.Log("non exist");
                spaceship.GetAnimator().SetTrigger("Damage");
            }
            //spaceship.GetAnimator().SetTrigger("Damage");
        }
	}

    public int getTouchDamage() { return touchDamage; }

	// Update is called once per frame
	void Update () {
	
	}
}
