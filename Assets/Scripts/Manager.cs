using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	//playerプレハブ
	public GameObject party;

	//タイトル
	private GameObject title;

	// Use this for initialization
	void Start () 
	{
		//Titleゲームオブジェクトを検索し取得する
		title=GameObject.Find("Title");
	}
	
    void OnGUI()
    {
        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (IsPlaying() == false && Event.current.type == EventType.MouseDown)
        {
            GameStart();
        }
    }

    /*
	// Update is called once per frame
	void Update () 
	{
        for (int i = 0; i < Input.touchCount; ++i)
        {
            //タッチ情報を取得する
            Touch touch = Input.GetTouch(i);

            //ゲーム中ではなく、タッチ直後であればtrueを返す。
            if (IsPlaying() == false && touch.phase==TouchPhase.Began)
            {
                GameStart();
            }
        }

        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (IsPlaying() == false && Input.GetMouseButtonDown(0))
        {
            GameStart();
        }

        /*
            //ゲーム中ではなく、Xキーを押されたらtrueを返す。
            if (IsPlaying() == false && Input.GetKeyDown(KeyCode.X))
            {
                GameStart();
            }
         
	}
    */


	public void GameOver()
	{
        // ハイスコアの保存
        FindObjectOfType<Score>().Save();

		//ゲームオーバー時に、タイトルを表示する
		title.SetActive (true);
	}

	void GameStart()
	{
        //delete enemy bullet
        GameObject[] enemyBullets;

        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet);
        }
        //delete player bullet
        GameObject[] playerBullets;

        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        foreach (GameObject playerBullet in playerBullets)
        {
            Destroy(playerBullet);
        }

        //ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        title.SetActive(false);

		Instantiate (party, party.transform.position, party.transform.rotation);
	}

	public bool IsPlaying()
	{
		//ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}
}
