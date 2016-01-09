using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Party : MonoBehaviour {

    // 移動スピード
    public float speed = 5;

    Background background;

	public enum Formation{
		Alex,
		Guylus,
		Nely,
		Rinmaru,
		Medhu
	};

	public Formation formation = Formation.Alex;

	Vector2 minPosition = new Vector2(-1,-1);
	Vector2 maxPosition = new Vector2(1,1);

	GameObject shieldObject;
	GameObject recoveryObject;

	public GameObject alex;
	public GameObject guylus;
	public GameObject nely;
	public GameObject rinmaru;
	public GameObject medhu;
	
	public bool nelyJoin = true;
	public bool rinmaruJoin = true;
	public bool medhuJoin = true;

	// Use this for initialization
	void Start () 
    {
        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        background = FindObjectOfType<Background>();

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
	}

	void CreateMember(GameObject chara){
		GameObject o = (GameObject)Instantiate(chara);
		o.transform.SetParent(transform);
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
				new Vector3(0.7f,0,0),
				new Vector3(0.3f,0.5f,0),
				new Vector3(0,0,0),
				new Vector3(0.3f,-0.5f,0),
				new Vector3(-0.6f,0,0)
			};
			break;
		case Formation.Guylus:
			positions = new Vector3[]{
				new Vector3(-0.2f,0.5f,0),
				new Vector3(0.5f,0,0),
				new Vector3(-0.2f,0,0),
				new Vector3(-0.2f,-0.5f,0),
				new Vector3(-0.6f,0,0)
			};
			break;
		case Formation.Nely:
			positions = new Vector3[]{
				new Vector3(-0.1f,0.5f,0),
				new Vector3(-0.5f,0.2f,0),
				new Vector3(0.5f,0,0),
				new Vector3(-0.1f,-0.5f,0),
				new Vector3(-0.5f,-0.2f,0)
			};
			break;
		case Formation.Rinmaru:
			positions = new Vector3[]{
				new Vector3(0.4f,0.4f,0),
				new Vector3(-0.4f,0.4f,0),
				new Vector3(0.4f,-0.4f,0),
				new Vector3(0,0,0),
				new Vector3(-0.4f,-0.4f,0)
			};
			break;
		case Formation.Medhu:
			positions = new Vector3[]{
				new Vector3(0.1f,0.3f,0),
				new Vector3(-0.3f,0.3f,0),
				new Vector3(0.1f,-0.3f,0),
				new Vector3(-0.3f,-0.3f,0),
				new Vector3(0.4f,0,0)
			};
			break;
		}
		for(int i=0; i<transform.childCount; ++i){
			if(i >= positions.Length){
				break;
			}
			transform.GetChild(i).position = transform.position + positions[i];
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

       

		Vector2 min = new Vector2(scale.x * -0.5f,scale.y*-0.2f);
        Vector2 max = scale * 0.5f;

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
    }
}
