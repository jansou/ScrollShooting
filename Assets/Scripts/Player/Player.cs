﻿using UnityEngine;
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

	public Party.Formation formation = Party.Formation.Alex;

    public bool isUndead = false;


	//public GameObject bullet;

	//Spaceshipコンポーネント
	Spaceship spaceship;

    //Backgroundコンポーネント
    //Background background;

	Transform shotposition;

    //nearHpRenderer:頭上の体力バー

	PlayerHP_Render hpRenderer;
	PlayerHP_Render nearHpRenderer;
	PlayerEXP_Render expRenderer;
	public string playerUIName;
	Text hpText;
	Text levelText;

	public bool init = false;
	public bool isPlayMode = false;

    //SE関係
    public AudioClip DamageSE;
    AudioSource audioSource;
    public AudioClip levelUpSE;
    public AudioClip GetCoinSE;

	IEnumerator Start()
	{
		while(init == false){
			yield return new WaitForEndOfFrame();
		}
		hp = HPByLevel(level);
		shotPower = PowerByLevel(level);
		maxHP = hp;

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = DamageSE;
        //

		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        //background = FindObjectOfType<Background>();

		shotposition = transform.GetChild(0);

		GameObject.Find(playerUIName).GetComponent<Button>().interactable = true;
        //HPgage関連
		SetUIWindowColor(new Color32(255,255,255,255));
		nearHpRenderer = transform.FindChild("HP_Gage").FindChild("HP").GetComponent<PlayerHP_Render>();
		nearHpRenderer.InitHP(hp);
		hpRenderer = getHPGauge(playerUIName);
		hpRenderer.InitHP(hp);
		expRenderer = getEXPGauge(playerUIName);
		expRenderer.InitEXP(preLevel(),exp,nextLevel());
		hpText = getHPText(playerUIName);
		levelText = getLevelText(playerUIName);

		hpText.text = hp.ToString()+"/"+maxHP.ToString();
		levelText.text = "Lv" + level.ToString();

		/*

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
		*/
		yield return null;
	}

	public void SetFormation(Party.Formation form){
		formation = form;
	}

	public void Attack(){
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
                shotposition.rotation = Quaternion.AngleAxis(0,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                //shotposition.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                //spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(90,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                //shotposition.rotation = Quaternion.AngleAxis(135, Vector3.forward);
                //spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(180,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                //shotposition.rotation = Quaternion.AngleAxis(225, Vector3.forward);
                //spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                //shotposition.rotation = Quaternion.AngleAxis(315, Vector3.forward);
                //spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
                /*
			if(type == Type.Rinmaru){
				shotposition.rotation = Quaternion.AngleAxis(0,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                shotposition.rotation = Quaternion.AngleAxis(45, Vector3.forward);
                spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(90,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                shotposition.rotation = Quaternion.AngleAxis(135, Vector3.forward);
                spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(180,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                shotposition.rotation = Quaternion.AngleAxis(225, Vector3.forward);
                spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                shotposition.rotation = Quaternion.AngleAxis(315, Vector3.forward);
                spaceship.Shot(shotposition, shotPower, 10, BulletManager.BulletType.PRinmaru);
			}
			else if(type == Type.Guylus){
                
				shotposition.rotation = Quaternion.AngleAxis(45,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
                
			}
			else if(type == Type.Medhu){
				shotposition.rotation = Quaternion.AngleAxis(135,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
			}
			else if(type == Type.Nely){
				shotposition.rotation = Quaternion.AngleAxis(225,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
			}
			else if(type == Type.Alex){
				shotposition.rotation = Quaternion.AngleAxis(315,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
			}
			else{
				shotposition.rotation = Quaternion.AngleAxis(270,Vector3.forward);
				spaceship.Shot(shotposition,shotPower,10,BulletManager.BulletType.PRinmaru);
			}
                */
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
	PlayerEXP_Render getEXPGauge(string playeUIName){
		return GameObject.Find (playerUIName).transform.FindChild("EXP_Gage").transform.FindChild("HP").GetComponent<PlayerEXP_Render>();
	}
	void SetUIWindowColor(Color32 c){
		GameObject.Find (playerUIName).transform.FindChild("Image").GetComponent<Image>().color = c;
	}


	void Update ()
	{
        
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D (Collider2D c)
	{
		//レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		//コイン取得
		if(c.tag == "Coin"){
			transform.parent.GetComponent<Party>().addMoney(c.GetComponent<Coin>().point);
            audioSource.PlayOneShot(GetCoinSE);
			Destroy(c.gameObject);
			return;
		}

		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") 
		{
            //ダメージ音
            audioSource.PlayOneShot(DamageSE);
			int damage = 0;

            if (layerName == "Bullet(Enemy)")
            {
                Bullet bullet = c.transform.GetComponent<Bullet>();
				damage = bullet.getDamage();
				if(bullet.isPenetrate == false){
                	Destroy(c.gameObject);
				}
            }

            if (layerName == "Enemy")
            {
                Enemy enemy = c.transform.GetComponent<Enemy>();
				damage = enemy.getTouchDamage();
            }

			damageHP(damage);
		}
	}



	public void damageHP(int damage)
    {
		
        if(isUndead)
        {
            hp = Mathf.Max(1, hp - damage);
        }
        else hp = Mathf.Max(0, hp - damage);
		
        
        if (hp <= 0)
		{
			//Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			//FindObjectOfType<Manager>().GameOver();
			
			//爆発する
			spaceship.Explosion();
			
			//プレイヤー削除
			Destroy(gameObject);

			SetUIWindowColor(new Color32(255,59,59,255));
			GameObject.Find(playerUIName).GetComponent<Button>().interactable = false;

			transform.parent.GetComponent<Party>().NotifyDeath(type);
		}
		else
		{
			spaceship.GetAnimator().SetTrigger("Damage");
		}

		FindObjectOfType<PopUp>().CreateText(transform.position+Vector3.up*0.2f,damage);
		nearHpRenderer.SetHP(hp);
		hpRenderer.SetHP(hp);
		hpText.text = hp.ToString()+"/"+maxHP.ToString();
	}

	public void RecoverPanel(){
		SetUIWindowColor(new Color32(255,255,255,255));
		GameObject.Find(playerUIName).GetComponent<Button>().interactable = true;
	}

    public void recoveryHP(int recover)
    {
		hp = Mathf.Min(hp+recover,maxHP);
		hpRenderer.SetHP(hp);
		hpText.text = hp.ToString()+"/"+maxHP.ToString();
	}

	public void addExp(int point)
	{
		exp += point;
		expRenderer.SetEXP(exp);
		if(exp >= nextLevel())
        {
            audioSource.PlayOneShot(levelUpSE);
			FindObjectOfType<PopUp>().CreateLevelUpText(transform.position);

			while (exp >= nextLevel())
			{
	            ++level;
	            shotPower = PowerByLevel(level);
	            maxHP = HPByLevel(level);
	            //体力全快
	            hp = maxHP;
				nearHpRenderer.InitHP(maxHP);
				hpRenderer.InitHP(maxHP);
				expRenderer.InitEXP(preLevel(), exp, nextLevel());
				levelText.text = "Lv" + level.ToString();
				hpText.text = hp.ToString()+"/"+maxHP.ToString();
			}
		}	
	}

	public void UseItem(ItemType item){
		switch(item){
		case ItemType.NormalHerb:
			recoveryHP(30);
			FindObjectOfType<MessageWindow>().showMessage("HPがかいふくした！");
			break;
		case ItemType.NiceHerb:
			recoveryHP(80);
			FindObjectOfType<MessageWindow>().showMessage("HPがかいふくした！");
			break;
		case ItemType.GreatHerb:
			recoveryHP(200);
			FindObjectOfType<MessageWindow>().showMessage("HPがかいふくした！");
			break;
		default:
			FindObjectOfType<MessageWindow>().showMessage("そのアイテムはまだ存在していません！");
			break;
		}
	}

	int nextLevel()
    {
		return needExpByLevel(level);
	}
	int preLevel(){
		return needExpByLevel(level-1);
	}

//HP,Power by Character
//キャラクター毎の調整を以下で行う(HP,攻撃力,必要経験値)

	int HPByLevel(int level)
    {
		switch(type)
        {
        case Type.Alex:
            return 12 + 3 * level;
		case Type.Guylus:
			return 15 + 4*level;
        case Type.Nely:
            return 10 + 2 * level;
		default:
			return 10 * level;
		}
	}
	int PowerByLevel(int level)
    {
        //Alex,
		//Guylus,
		//Nely,

		switch(type){
            case Type.Alex:
                return 1 + (level / 5);
            case Type.Guylus:
                return 1 + (level / 6);
            case Type.Nely:
                return 1 + (level / 4);
		    default:
			    return 1 + (level/4);
		}
	}
	int needExpByLevel(int level){
        return (200 * (level*2 - 1));
	}
}