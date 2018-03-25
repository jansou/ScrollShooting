using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Party : MonoBehaviour {

    // 移動スピード
    public float speed = 5;

    Background background;

    ///陣形の位置情報
	public enum Formation{
		Alex,
		Guylus,
		Nely,
		Rinmaru,
		Medhu
	};

    public bool isArcade=false;
    public bool isUndead = false;

    //Alex
    public Vector3 AlexPos0;// = new Vector3(0.7f,0,0);
	public Vector3 AlexPos1;// = new Vector3(0.3f,0.5f,0);
	public Vector3 AlexPos2;// = new Vector3(0,0,0);
    public Vector3 AlexPos3;// = new Vector3(0.3f, -0.5f, 0);
	public Vector3 AlexPos4;// = new Vector3(-0.6f,0,0);

    //Guylus
	public Vector3 GuylusPos0;// = new Vector3(-0.2f,0.5f,0);
	public Vector3 GuylusPos1;// = new Vector3(0.5f,0,0);
	public Vector3 GuylusPos2;// = new Vector3(-0.2f,0,0);
	public Vector3 GuylusPos3;// = new Vector3(-0.2f,-0.5f,0);
    public Vector3 GuylusPos4;// = new Vector3(-0.6f, 0, 0);

    //Nely
	public Vector3 NelyPos0;// = new Vector3(-0.1f,0.5f,0);
	public Vector3 NelyPos1;// = new Vector3(-0.5f,0.2f,0);
	public Vector3 NelyPos2;// = new Vector3(0.5f,0,0);
	public Vector3 NelyPos3;// = new Vector3(-0.1f,-0.5f,0);
    public Vector3 NelyPos4;// = new Vector3(-0.5f, -0.2f, 0);

    //Rinmaru
	public Vector3 RinmaruPos0 = new Vector3(0.4f,0.4f,0);
	public Vector3 RinmaruPos1 = new Vector3(-0.4f,0.4f,0);
	public Vector3 RinmaruPos2 = new Vector3(0.4f,-0.4f,0);
	public Vector3 RinmaruPos3 = new Vector3(0,0,0);
    public Vector3 RinmaruPos4 = new Vector3(-0.4f, -0.4f, 0);

    //Vector3[] RinmaruPositions = new Vector3[]{ RinmaruPos0, RinmaruPos1, RinmaruPos2, RinmaruPos3, RinmaruPos4 };

    //Medhu
	public Vector3 MedhuPos0 = new Vector3(0.1f,0.3f,0);
    public Vector3 MedhuPos1 = new Vector3(-0.3f, 0.3f, 0);
	public Vector3 MedhuPos2 = new Vector3(0.1f,-0.3f,0);
	public Vector3 MedhuPos3 = new Vector3(-0.3f,-0.3f,0);
	public Vector3 MedhuPos4 = new Vector3(0.4f,0,0);
    /// /////////////////////////////////////////////////////////
   
	public Formation formation = Formation.Alex;

	Vector2 minPosition = new Vector2(-1,-1);
	Vector2 maxPosition = new Vector2(1,1);

	GameObject shieldObject;
	GameObject recoveryObject;

	public GameObject alexPrefab;
	public GameObject guylusPrefab;
	public GameObject nelyPrefab;
	public GameObject rinmaruPrefab;
	public GameObject medhuPrefab;
	GameObject alex;
	GameObject guylus;
	GameObject nely;
	GameObject rinmaru;
	GameObject medhu;
	SaveManager.Status alexStatus;
	SaveManager.Status guylusStatus;
	SaveManager.Status nelyStatus;
	SaveManager.Status rinmaruStatus;
	SaveManager.Status medhuStatus;

    public bool alexJoin = true;
    public bool guylusJoin = true;
	public bool nelyJoin = true;
	public bool rinmaruJoin = true;
	public bool medhuJoin = true;

	private bool isPlayMode  =false; // true..start move and shot

	int nowMoney = 0;

	public void SetPlayMode(bool b)
    {
		isPlayMode = b;
        Debug.Log("プレイモード→" + b);

        for(int i=0; i<transform.childCount; ++i)
        {
			transform.GetChild(i).GetComponent<Player>().isPlayMode = b;
		}
	}

	// Use this for initialization
	void Start () 
    {
        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        background = FindObjectOfType<Background>();
	}

    public void SetMember()
    {
		if (alexJoin){
			alex = CreateMember(alexPrefab);
		}
		if (guylusJoin){
			guylus = CreateMember(guylusPrefab);

            shieldObject = transform.FindChild("Guylus(Clone)").FindChild("shield").gameObject;
		}
		if (nelyJoin){
			nely = CreateMember(nelyPrefab);
		}
		if (rinmaruJoin){
			rinmaru = CreateMember(rinmaruPrefab);
		}
		if (medhuJoin){
			medhu = CreateMember(medhuPrefab);
		}
        if (medhuJoin)
        {
            recoveryObject = transform.FindChild("Medhu(Clone)").FindChild("recoveryField").gameObject;
        }

        SetFormation(formation);

		LoadPartyFromSave();

		StartCoroutine("ShotRoutine");
    }
	public void SaveParty(){
		SaveManager sm = FindObjectOfType<SaveManager>();
		if(sm == null){
			Debug.LogWarning("SaveManager can't found");
			return;
		}

		sm.money = nowMoney;

		if(alexJoin){
			sm.alex = alexStatus;
		}
		if(guylusJoin){
			sm.guylus = guylusStatus;
		}
		if(nelyJoin){
			sm.nely = nelyStatus;
		}
		if(rinmaruJoin){
			sm.rinmaru = rinmaruStatus;
		}
		if(medhuJoin){
			sm.medhu = medhuStatus;
		}
		sm.SaveData();
	}
	SaveManager.Status GetCharaStatus(Player p){
		SaveManager.Status st;
		st.level = p.level;
		st.exp = p.exp;
		st.isJoining = true;
		return st;
	}
	void LoadPartyFromSave(){
		SaveManager sm = FindObjectOfType<SaveManager>();
		if(!sm.IsLoaded()){
			sm.Load();
		}

		nowMoney = sm.money;

		if(alexJoin){
			alexStatus = sm.alex;
			LoadCharaFromSave(alex.GetComponent<Player>(),sm.alex);
		}
		if(guylusJoin){
			guylusStatus = sm.guylus;
			LoadCharaFromSave(guylus.GetComponent<Player>(),sm.guylus);
		}
		if(nelyJoin){
			nelyStatus = sm.nely;
			LoadCharaFromSave(nely.GetComponent<Player>(),sm.nely);
		}
		if(rinmaruJoin){
			rinmaruStatus = sm.rinmaru;
			LoadCharaFromSave(rinmaru.GetComponent<Player>(),sm.rinmaru);
		}
		if(medhuJoin){
			medhuStatus = sm.medhu;
			LoadCharaFromSave(medhu.GetComponent<Player>(),sm.medhu);
		}
	}

	void LoadCharaFromSave(Player p,SaveManager.Status st)
    {
        if (!isArcade)
        {
            p.level = st.level;
            p.exp = st.exp;
        }

        //体力が1未満にならない
        p.isUndead = isUndead;

        p.init = true;
	}

	GameObject CreateMember(GameObject chara)
    {
		GameObject o = (GameObject)Instantiate(chara);
		o.transform.SetParent(transform);
		return o;
	}

	void debug(){
		//全員の行動を止める
		if(Input.GetKeyDown(KeyCode.S))
        {
            bool b = isPlayMode ? false : true;
            Debug.Log("[Party.cs] Stop→" + b);

			for(int i=0; i<transform.childCount; ++i)
            {
				SetPlayMode(b);
			}
		}

		//味方を全員HP1に
		if(Input.GetKeyDown(KeyCode.R)){
			for(int i=0; i<transform.childCount; ++i){
				Debug.Log ("[Party.cs]All Party HP 1");
				transform.GetChild(i).GetComponent<Player>().hp = 1;
			}
		}
		//敵を全員HP1に
		if(Input.GetKeyDown(KeyCode.A)){
			for(int i=0; i<transform.childCount; ++i){
				Debug.Log ("[Party.cs]All Enemy HP 1");
				foreach(var enemy in FindObjectsOfType<Enemy>()){
					enemy.hp = 1;
				}
			}
		}
	}

	IEnumerator ShotRoutine(){
		while(isPlayMode == false){
			yield return new WaitForEndOfFrame();
		}
		
		yield return new WaitForEndOfFrame();// for SpaceShip.Start()
		
		while (true) 
		{
			if (isPlayMode) {
				if(alex){
					alex.GetComponent<Player>().Attack();
				}
				if(guylus){
					guylus.GetComponent<Player>().Attack();
				}
				if(nely){
					nely.GetComponent<Player>().Attack();
				}
				if(rinmaru){
					rinmaru.GetComponent<Player>().Attack();
				}
				if(medhu){
					medhu.GetComponent<Player>().Attack();
				}
				if(formation != Formation.Medhu){
					GetComponent<AudioSource>().Play();
				}
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
	// Update is called once per frame
	void Update () 
    {
		debug ();

		if(isPlayMode){
	        // 右・左
	        //float x = Input.GetAxisRaw ("Horizontal");
	        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

	        // 上・下
	        //float y = Input.GetAxisRaw ("Vertical");
	        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");

	        // 移動する向きを求める
	        //Vector2 direction = new Vector2 (x, y).normalized;
	        Vector2 direction = new Vector2(x, y);
	        Move(direction);
			

	        // 移動する向きとスピードを代入する
	        //GetComponent<Rigidbody2D>().velocity = direction * speed;
	        //spaceship.Move (direction);

	        //移動の制限(だめな例)
	        //Clamp ();
		}

		//GameOver判定(子オブジェクトが０になったら)
		if (this.transform.childCount==0)
		{
			//Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			FindObjectOfType<Manager>().GameOver();
			Destroy(this.gameObject);
		}

	}

	public void SetFormation(Formation formation){
		Vector3[] positions = null;
		/*
		 * positions = new Vector3[]{
		 *  alex
		 *  guylus,
		 *  nely,
		 *  rinmaru,
		 *  medhu
		 * };
		*/
		switch(formation){
		case Formation.Alex:
			positions = new Vector3[]{
                
				AlexPos0,//new Vector3(0.7f,0,0),
				AlexPos1,//new Vector3(0.3f,0.5f,0),
				AlexPos2,//new Vector3(0,0,0),
				AlexPos3,//new Vector3(0.3f,-0.5f,0),
				AlexPos4,//new Vector3(-0.6f,0,0)
                
			};
			GetComponent<AudioSource>().clip = alex.GetComponent<AudioSource>().clip;
			break;
		case Formation.Guylus:
			positions = new Vector3[]{
				GuylusPos0 ,//new Vector3(-0.2f,0.5f,0),
				GuylusPos1 ,//new Vector3(0.5f,0,0),
				GuylusPos2 ,//new Vector3(-0.2f,0,0),
				GuylusPos3 ,//new Vector3(-0.2f,-0.5f,0),
				GuylusPos4 ,//new Vector3(-0.6f,0,0)
			};
			GetComponent<AudioSource>().clip = guylus.GetComponent<AudioSource>().clip;
			break;
		case Formation.Nely:
			positions = new Vector3[]{
				NelyPos0,//new Vector3(-0.1f,0.5f,0),
				NelyPos1,//new Vector3(-0.5f,0.2f,0),
				NelyPos2,//new Vector3(0.5f,0,0),
				NelyPos3,//new Vector3(-0.1f,-0.5f,0),
				NelyPos4,//new Vector3(-0.5f,-0.2f,0)
			};
			GetComponent<AudioSource>().clip = nely.GetComponent<AudioSource>().clip;
			break;
		case Formation.Rinmaru:
            positions = //RinmaruPositions;
               
            new Vector3[]{
				RinmaruPos0,//new Vector3(0.4f,0.4f,0),
				RinmaruPos1,//new Vector3(-0.4f,0.4f,0),
				RinmaruPos2,//new Vector3(0.4f,-0.4f,0),
				RinmaruPos3,//new Vector3(0,0,0),
				RinmaruPos4,//new Vector3(-0.4f,-0.4f,0)
			};
			GetComponent<AudioSource>().clip = rinmaru.GetComponent<AudioSource>().clip;
             
			break;
		case Formation.Medhu:
			positions = new Vector3[]{
				MedhuPos0,//new Vector3(0.1f,0.3f,0),
				MedhuPos1,//new Vector3(-0.3f,0.3f,0),
				MedhuPos2,//new Vector3(0.1f,-0.3f,0),
				MedhuPos3,//new Vector3(-0.3f,-0.3f,0),
				MedhuPos4,//new Vector3(0.4f,0,0)
			};
			GetComponent<AudioSource>().clip = medhu.GetComponent<AudioSource>().clip;
			break;
		}

		if(alex){
			alex.transform.position = transform.position + positions[0];
		}
		if(guylus){
			guylus.transform.position = transform.position + positions[1];
		}
		if(nely){
			nely.transform.position = transform.position + positions[2];
		}
		if(rinmaru){
			rinmaru.transform.position = transform.position + positions[3];
		}
		if(medhu){
			medhu.transform.position = transform.position + positions[4];
		}

		this.formation = formation;
		for(int i=0; i<transform.childCount; ++i){
			transform.GetChild(i).GetComponent<Player>().SetFormation(formation);
		}

		if(shieldObject){
			if(formation == Formation.Guylus){
				shieldObject.SetActive(true);
			}
			else{
				shieldObject.SetActive(false);
			}
		}
		if(recoveryObject){
			if(formation == Formation.Medhu){
				recoveryObject.SetActive(true);
			}
			else{
				recoveryObject.SetActive(false);
			}
		}
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

       

		Vector2 min = new Vector2(scale.x * -0.55f,scale.y *-0.3f );
        Vector2 max = new Vector2(scale.x * 0.55f, scale.y * 0.5f); 

		min.x += 1;
		max.x -= 1;

		min -= minPosition;
		max -= maxPosition;



        //プレイヤー座標の取得
        Vector3 pos = transform.position;

        //移動量を加える
        //pos += direction * spaceship.speed * Time.deltaTime;
        pos += direction * speed * Time.deltaTime;

        //プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //制限をかけた値をプレイヤーの位置とする
        transform.position = pos;
    }


	public void ChangeFormation(){
		if(formation == Formation.Alex){
			SetFormation(Formation.Guylus);
		}
		else if(formation == Formation.Guylus){
			SetFormation(Formation.Nely);
		}
		else if(formation == Formation.Nely){
			SetFormation(Formation.Rinmaru);
		}
		else if(formation == Formation.Rinmaru){
			SetFormation(Formation.Medhu);
		}
		else{
			SetFormation(Formation.Alex);
		}
	}


    public void addExp(int point)
    {
        Player[] playerList;
        playerList = GetComponentsInChildren<Player>();

        //GameObject party = GameObject.Find("party");

        //経験値を全プレイヤーに追加
        foreach (Player player in playerList)
        {
            player.addExp(point);
        }


		if(alex){
			alexStatus = GetCharaStatus(alex.GetComponent<Player>());
		}
		if(guylus){
			guylusStatus = GetCharaStatus(guylus.GetComponent<Player>());
		}
		if(nely){
			nelyStatus = GetCharaStatus(nely.GetComponent<Player>());
		}
		if(rinmaru){
			rinmaruStatus = GetCharaStatus(rinmaru.GetComponent<Player>());
		}
		if(medhu){
			medhuStatus = GetCharaStatus(medhu.GetComponent<Player>());
		}
    }


	void SetMark(Transform t,string UIName,bool b){
		t.FindChild(UIName).FindChild("LeaderMark").GetComponent<Image>().enabled = b;
	}

	public void addMoney(int m){
		nowMoney += m;
	}

	public void UseItemToMember(ItemType item,int num){
		GameObject member = MemberByNum(num);
		member.GetComponent<Player>().UseItem(item);
	}

	GameObject MemberByNum(int num){
		switch(num){
		case 0:
			return alex;
		case 1:
			return guylus;
		case 2:
			return nely;
		case 3:
			return rinmaru;
		case 4:
			return medhu;
		default:
			return null;
		}
	}
	GameObject PrefabByNum(int num){
		switch(num){
		case 0:
			return alexPrefab;
		case 1:
			return guylusPrefab;
		case 2:
			return nelyPrefab;
		case 3:
			return rinmaruPrefab;
		case 4:
			return medhuPrefab;
		default:
			return null;
		}
	}
	SaveManager.Status StatusByNum(SaveManager sm,int num){
		switch(num){
		case 0:
			return sm.alex;
		case 1:
			return sm.guylus;
		case 2:
			return sm.nely;
		case 3:
			return sm.rinmaru;
		case 4:
			return sm.medhu;
		default:
			return sm.alex;
		}
	}

	public bool CanUseItem(ItemType item,int num){
		if(item == ItemType.GreatLifeOrb){
			return 
				(alexJoin && alex==null) || 
				(guylusJoin && guylus==null) || 
				(nelyJoin && nely==null) || 
				(rinmaruJoin && rinmaru==null) || 
				(medhuJoin && medhu==null);
		}
		else{
			GameObject member = MemberByNum(num);
			return CanUseItemToObject(item,member);
		}
	}
	bool CanUseItemToObject(ItemType item,GameObject member){
		switch(item){
		case ItemType.NormalHerb:
		case ItemType.NiceHerb:
		case ItemType.GreatHerb:
			return member != null;
		case ItemType.LifeOrb:
			return member == null;
		default:
			return false;
		}
	}

	public void UseReviveItemToAll(){
		if(alexJoin && alex == false){
			UseReviveItemToMember(ItemType.LifeOrb,0);
		}
		if(guylusJoin && guylus == false){
			UseReviveItemToMember(ItemType.LifeOrb,1);
		}
		if(nelyJoin && nely == false){
			UseReviveItemToMember(ItemType.LifeOrb,2);
		}
		if(rinmaruJoin && rinmaru == false){
			UseReviveItemToMember(ItemType.LifeOrb,3);
		}
		if(medhuJoin && medhu == false){
			UseReviveItemToMember(ItemType.LifeOrb,4);
		}
	}

	public void UseReviveItemToMember(ItemType item,int num){
		SaveManager sm = FindObjectOfType<SaveManager>();
		GameObject member = MemberByNum(num);

		if(member){
			Debug.LogError("まだ生きているメンバーです");
		}
		else{
			member = CreateMember(PrefabByNum(num));
			Player p = member.GetComponent<Player>();
			LoadCharaFromSave(p,StatusByNum(sm,num));
			p.RecoverPanel();
			p.isPlayMode = true;

			switch(num){
			case 0:
				alex = member;
				break;
			case 1:
				guylus = member;
				break;
			case 2:
				nely = member;
				break;
			case 3:
				rinmaru = member;
				break;
			case 4:
				medhu = member;
				break;

			}

			Player.Type t = TypeByNum(num);
			if(t == Player.Type.Guylus){
				shieldObject = transform.FindChild("Guylus(Clone)").FindChild("shield").gameObject;
			}
			if(t == Player.Type.Medhu){
				recoveryObject = transform.FindChild("Medhu(Clone)").FindChild("recoveryField").gameObject;
			}
		}
		SetFormation(formation);
	}

	Player.Type TypeByNum(int num){
		switch(num){
		case 0:
			return Player.Type.Alex;
		case 1:
			return Player.Type.Guylus;
		case 2:
			return Player.Type.Nely;
		case 3:
			return Player.Type.Rinmaru;
		case 4:
			return Player.Type.Medhu;
		default:
			return Player.Type.Alex;
		}
	}

	Formation FormationByType(Player.Type type){
		switch(type){
		case Player.Type.Alex:
			return Formation.Alex;
		case Player.Type.Guylus:
			return Formation.Guylus;
		case Player.Type.Nely:
			return Formation.Nely;
		case Player.Type.Rinmaru:
			return Formation.Rinmaru;
		case Player.Type.Medhu:
			return Formation.Medhu;
		default:
			return Formation.Alex;
		}
	}

	//メンバーが倒れたらフォーメーションを強制変更
	public void NotifyDeath(Player.Type type){
		bool changeForm = false;
		changeForm = formation == FormationByType(type);

		if(changeForm){
			Transform t = GameObject.Find("Players").transform;
			SetMark (t,"1stPlayer",false);
			SetMark (t,"2ndPlayer",false);
			SetMark (t,"3rdPlayer",false);
			SetMark (t,"4thPlayer",false);
			SetMark (t,"5thPlayer",false);

			if(alex && type != Player.Type.Alex){
				SetMark (t,"1stPlayer",true);
				SetFormation(Formation.Alex);
			}
			else if(guylus && type != Player.Type.Guylus){
				SetMark (t,"2ndPlayer",true);
				SetFormation(Formation.Guylus);
			}
			else if(nely && type != Player.Type.Nely){
				SetMark (t,"3rdPlayer",true);
				SetFormation(Formation.Nely);
			}
			else if(rinmaru && type != Player.Type.Rinmaru){
				SetMark (t,"4thPlayer",true);
				SetFormation(Formation.Rinmaru);
			}
			else if(medhu && type != Player.Type.Medhu){
				SetMark (t,"5thPlayer",true);
				SetFormation(Formation.Medhu);
			}
		}
	}
}
