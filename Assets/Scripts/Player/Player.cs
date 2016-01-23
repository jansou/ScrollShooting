using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public enum Type{
		Alex,
		Guylus,
		Nely,
		Rinmaru,
		Medhu
	};
	public Type type;
	// 移動スピード
	public float speed = 5;

    public int level = 1;

    public int hp = 10;
	int maxHP;

    public int shotPower=1;

    public int exp=0;

	Party.Formation formation = Party.Formation.Alex;

	//public GameObject bullet;

	//Spaceshipコンポーネント
	Spaceship spaceship;

    //Backgroundコンポーネント
    //Background background;

	Transform shotposition;

	PlayerHP_Render hpRenderer;
	public string playerUIName;
	Text hpText;
	Text levelText;

	public bool init = false;
	public bool isPlayMode = false;

	IEnumerator Start()
	{
		while(init == false){
			yield return new WaitForEndOfFrame();
		}
        hp *= level;
		maxHP = hp;

		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        //background = FindObjectOfType<Background>();

		shotposition = transform.GetChild(0);

		hpRenderer = getHPGauge(playerUIName);
		hpRenderer.InitHP(hp);
		hpText = getHPText(playerUIName);
		levelText = getLevelText(playerUIName);

		hpText.text = hp.ToString()+"/"+maxHP.ToString();
		levelText.text = "Lv" + level.ToString();

		while(isPlayMode == false){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();// for SpaceShip.Start()

		while (isPlayMode == true) 
		{
			Attack();
			//Instantiate(bullet, transform.position,transform.rotation);

			GetComponent<AudioSource>().Play();

			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}

	public void SetFormation(Party.Formation form){
		formation = form;
	}

	void Attack(){
		switch(formation){
		case Party.Formation.Alex:
			shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
			spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.Player);
			shotposition.rotation = Quaternion.AngleAxis(270+15,Vector3.forward);
			spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.Player);
			shotposition.rotation = Quaternion.AngleAxis(270-15,Vector3.forward);
			spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.Player);

            //yield return new WaitForSeconds(spaceship.shotDelay);
			break;
		case Party.Formation.Guylus:
			if(type != Type.Guylus){
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PGuylus);
	            shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PGuylus,0,-0.3f);
	            shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PGuylus,0,0.3f);
			}

            //yield return new WaitForSeconds(spaceship.shotDelay);
			break;
		case Party.Formation.Nely:
			shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
			spaceship.Shot(shotposition,shotPower,5,BulletManager.BulletType.PHoming);

            //yield return new WaitForSeconds(spaceship.shotDelay);
			break;
		case Party.Formation.Rinmaru:
			if(type == Type.Rinmaru){
				shotposition.rotation = Quaternion.AngleAxis(0,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
				shotposition.rotation = Quaternion.AngleAxis(90,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
				shotposition.rotation = Quaternion.AngleAxis(180,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}
			else if(type == Type.Guylus){
				shotposition.rotation = Quaternion.AngleAxis(45,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}
			else if(type == Type.Medhu){
				shotposition.rotation = Quaternion.AngleAxis(135,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}
			else if(type == Type.Nely){
				shotposition.rotation = Quaternion.AngleAxis(225,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}
			else if(type == Type.Alex){
				shotposition.rotation = Quaternion.AngleAxis(315,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}
			else{
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PSlash);
			}

            //yield return new WaitForSeconds(spaceship.shotDelay);
			break;
		case Party.Formation.Medhu:
			break;
		default:
			shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
			spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.Player);

            //yield return new WaitForSeconds(spaceship.shotDelay);
			break;
		}
	}

	PlayerHP_Render getHPGauge(string playeUIName){
		return GameObject.Find (playerUIName).transform.FindChild("PlayerHP_Gage").transform.FindChild("HP").GetComponent<PlayerHP_Render>();
	}
	Text getHPText(string playerUIName){
		return GameObject.Find (playerUIName).transform.FindChild("HP").GetComponent<Text>();
	}
	Text getLevelText(string playerUIName){
		return GameObject.Find (playerUIName).transform.FindChild("Level").GetComponent<Text>();
	}


	void Update ()
	{
        /*
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
         */
	}

    /*
	void Move(Vector3 direction)
	{
        //背景のスケール
        Vector2 scale = background.transform.localScale;
        
		//画面左下のワールド座標をビューポートから取得
		//Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		//画面右上のワールド座標をビューポートから取得
		//Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        

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
    */

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

		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") 
		{
			int damage = 0;

            if (layerName == "Bullet(Enemy)")
            {
                Bullet bullet = c.transform.GetComponent<Bullet>();
				damage = bullet.getDamage();

                Destroy(c.gameObject);
            }

            if (layerName == "Enemy")
            {
                Enemy enemy = c.transform.GetComponent<Enemy>();
				damage = enemy.getTouchDamage();
            }

			damageHP(damage);
		}
	}

	public void damageHP(int damage){
		hp = Mathf.Max(0, hp - damage);
		if (hp <= 0)
		{
			//Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			//FindObjectOfType<Manager>().GameOver();
			
			//爆発する
			spaceship.Explosion();
			
			//プレイヤー削除
			Destroy(gameObject);
		}
		else
		{
			spaceship.GetAnimator().SetTrigger("Damage");
		}

		FindObjectOfType<PopUp>().CreateText(transform.position+Vector3.up*0.2f,damage);
		hpRenderer.SetHP(hp);
		hpText.text = hp.ToString()+"/"+maxHP.ToString();
	}
	public void recoveryHP(int recover){
		hp = Mathf.Min(hp+recover,maxHP);
		hpRenderer.SetHP(hp);
	}

	int nextLevel(){
		return 100 * level * level;
	}

    public void addExp(int point)
    {
        exp += point;
		if(exp >= nextLevel()){
			++level;
			FindObjectOfType<PopUp>().CreateText(transform.position,"LEVEL UP");
			levelText.text = "Lv" + level.ToString();
		}

    }

}