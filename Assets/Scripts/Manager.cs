using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	//playerプレハブ
	public GameObject party;
	GameObject createdParty;

	//タイトル
	private GameObject title;

	private GameObject clear;

	private GameObject pause;

	bool paused = false;

	// Use this for initialization
	void Start () 
	{
		//Titleゲームオブジェクトを検索し取得する
		title=GameObject.Find("Title");
		clear=GameObject.Find("ClearCanvas");
		pause=GameObject.Find("PauseCanvas");

		clear.SetActive(false);
		pause.SetActive(false);
	}
	
    void OnGUI()
    {
        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (IsPlaying() == false && clear.activeSelf == false && Event.current.type == EventType.MouseDown)
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

	public void GameClear()
	{
		clear.SetActive(true);
		FindObjectOfType<Score>().Save();
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

		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			Destroy(enemy);
		}
		FindObjectOfType<Emitter>().ResetWave();

        //ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        title.SetActive(false);

		createdParty = (GameObject)Instantiate (party, party.transform.position, party.transform.rotation);
	}

	public bool IsPlaying()
	{
		//ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false && clear.activeSelf == false;
	}

	public void GamePause(){
		if(paused){
			paused = false;
			pause.SetActive(false);
			Time.timeScale = 1;
		}
		else{
			paused = true;
			pause.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public void SetPartyForm(int formnum){
		if(createdParty){
			Party.Formation form = Party.Formation.Alex;
			switch(formnum){
			case 0:
				form = Party.Formation.Alex;
				break;
			case 1:
				form = Party.Formation.Guylus;
				break;
			}
			createdParty.GetComponent<Party>().SetFormation(form);
		}
	}
}
