using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Party : MonoBehaviour {

    // 移動スピード
    public float speed = 5;

    Background background;

	public enum Formation{
		Normal,
		Line,
	};

	public Formation formation = Formation.Line;

	Vector2 minPosition = new Vector2(-1,-1);
	Vector2 maxPosition = new Vector2(1,1);


	// Use this for initialization
	void Start () 
    {
        //Backgroundコンポーネントを取得。３つのうちどれか一つを取得する
        background = FindObjectOfType<Background>();
	
		SetFormation(formation);


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

	}

	void SetFormation(Formation formation){
		switch(formation){
		case Formation.Normal:
			transform.GetChild(0).position = transform.position + new Vector3(0,-1,0);
			transform.GetChild(1).position = transform.position + new Vector3(0,1,0);
			break;
		case Formation.Line:
			transform.GetChild(0).position = transform.position + new Vector3(1,0,0);
			transform.GetChild(1).position = transform.position + new Vector3(-1,0,0);
			break;
		}

		this.formation = formation;
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

		min.x += 1;
		max.x -= 1;

		min -= minPosition;
		max -= maxPosition;

		Debug.Log(min);
		Debug.Log (max);


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
		if(formation == Formation.Normal){
			SetFormation(Formation.Line);
		}
		else if(formation == Formation.Line){
			SetFormation(Formation.Normal);
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
