using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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

	// Use this for initialization
	void Start () 
    {
        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        background = FindObjectOfType<Background>();

        /*
		CreateMember(alex);
		CreateMember(guylus);
		
        if(nelyJoin) CreateMember(nely);
		if(rinmaruJoin) CreateMember(rinmaru);
		if(medhuJoin) CreateMember(medhu);

		shieldObject = transform.FindChild("Guylus(Clone)").FindChild("shield").gameObject;
		if(medhuJoin){
			recoveryObject = transform.FindChild("Medhu(Clone)").FindChild("recoveryField").gameObject;
		}
		SetFormation(formation);
        */
	}

    public void SetMember()
    {
		if (alexJoin){
			alex = CreateMember(alexPrefab);
		}
		if (guylusJoin){
			guylus = CreateMember(guylusPrefab);
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

        shieldObject = transform.FindChild("Guylus(Clone)").FindChild("shield").gameObject;
        if (medhuJoin)
        {
            recoveryObject = transform.FindChild("Medhu(Clone)").FindChild("recoveryField").gameObject;
        }
        SetFormation(formation);

		LoadPartyFromSave();
    }
	public void SaveParty(){
		SaveManager sm = FindObjectOfType<SaveManager>();
		if(sm == null){
			Debug.LogWarning("SaveManager can't found");
			return;
		}
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
	void LoadCharaFromSave(Player p,SaveManager.Status st){
		p.level = st.level;
		p.exp = st.exp;
	}

	GameObject CreateMember(GameObject chara){
		GameObject o = (GameObject)Instantiate(chara);
		o.transform.SetParent(transform);
		return o;
	}
	
	// Update is called once per frame
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
        Move(direction);

        // 移動する向きとスピードを代入する
        //GetComponent<Rigidbody2D>().velocity = direction * speed;
        //spaceship.Move (direction);

        //移動の制限(だめな例)
        //Clamp ();

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
			break;
		case Formation.Guylus:
			positions = new Vector3[]{
				GuylusPos0 ,//new Vector3(-0.2f,0.5f,0),
				GuylusPos1 ,//new Vector3(0.5f,0,0),
				GuylusPos2 ,//new Vector3(-0.2f,0,0),
				GuylusPos3 ,//new Vector3(-0.2f,-0.5f,0),
				GuylusPos4 ,//new Vector3(-0.6f,0,0)
			};
			break;
		case Formation.Nely:
			positions = new Vector3[]{
				NelyPos0,//new Vector3(-0.1f,0.5f,0),
				NelyPos1,//new Vector3(-0.5f,0.2f,0),
				NelyPos2,//new Vector3(0.5f,0,0),
				NelyPos3,//new Vector3(-0.1f,-0.5f,0),
				NelyPos4,//new Vector3(-0.5f,-0.2f,0)
			};
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
             
			break;
		case Formation.Medhu:
			positions = new Vector3[]{
				MedhuPos0,//new Vector3(0.1f,0.3f,0),
				MedhuPos1,//new Vector3(-0.3f,0.3f,0),
				MedhuPos2,//new Vector3(0.1f,-0.3f,0),
				MedhuPos3,//new Vector3(-0.3f,-0.3f,0),
				MedhuPos4,//new Vector3(0.4f,0,0)
			};
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
}
