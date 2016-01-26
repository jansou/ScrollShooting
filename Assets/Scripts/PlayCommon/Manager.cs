using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour 
{
	//playerプレハブ
	public GameObject party;
	GameObject createdParty;

	//タイトル
	private GameObject title;

	public GameObject clear;

	public GameObject gameover;

	public GameObject pause;

    //闘技場用
    public bool isArcade=false;

	bool paused = false;

    public bool joinAlex = false;
    public bool joinGuylus = false;
    public bool joinNely = false;
    public bool joinRinmaru = false;
    public bool joinMedhu = false;
	public int stageNumber = 0;

    //チュートリアル用の不死設定
    public bool isUndead=false;


    //SE関係
    public AudioClip tapSE;
    public AudioClip gamestartSE;
    public AudioClip gameclearSE;
    public AudioClip gameoverSE;
    AudioSource audioSource;
    ///

	// Use this for initialization
	void Start () 
	{
        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        //

		//Titleゲームオブジェクトを検索し取得する
		title=GameObject.Find("Title");
		clear=GameObject.Find("ClearCanvas");
		pause=GameObject.Find("PauseCanvas");
		gameover=GameObject.Find("GameOverCanvas");

		Transform playerUIs = GameObject.Find("Players").transform;

		if(!joinNely){
			playerUIs.Find("filter3").GetComponent<Image>().enabled = true;
		}
		if(!joinRinmaru){
			playerUIs.Find("filter4").GetComponent<Image>().enabled = true;
		}
		if(!joinMedhu){
			playerUIs.Find("filter5").GetComponent<Image>().enabled = true;
		}

		gameover.SetActive(false);
		clear.SetActive(false);
		pause.SetActive(false);

		GameStart();
	}
	IEnumerator AppearParty(){
		int f = 0;
		while(f < 60){
			createdParty.transform.Translate(0.03f,0,0);
			++f;
			yield return new WaitForEndOfFrame();
		}
		title.SetActive(false);
		createdParty.GetComponent<Party>().SetPlayMode(true);
	}

	IEnumerator LeaveParty(){
		int f = 0;
		while(createdParty.transform.position.x < 7){
			if(f > 50){
				createdParty.transform.Translate(0.1f,0,0);
			}
			++f;
			yield return new WaitForEndOfFrame();
		}
	}
	
    void OnGUI()
    {

		/*
        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (IsPlaying() == false && gameover.activeSelf == true && Event.current.type == EventType.MouseDown)
        {

        }
        */
        
    }

	public void GameRestart()
	{
        //audioSource.Stop(gameoverSE);
		GameStart();
	}

	public void GameOver()
	{
        FindObjectOfType<BGMManager>().StopBGM();
        //ゲームオーバー時のSE
        audioSource.PlayOneShot(gameoverSE);

        if (!isArcade)//アーケードモードの場合は保存しない
        {
            // 経験値の保存
            FindObjectOfType<Score>().Save();
            createdParty.GetComponent<Party>().SaveParty();
        }
		//ゲームオーバー時に、タイトルを表示する
		gameover.SetActive (true);

	}

	public void GameExit(){
		Time.timeScale = 1; // for pause

        //だめかも
        audioSource.PlayOneShot(tapSE);

        /*
        if(pause.activeSelf)
        {
			createdParty.GetComponent<Party>().SaveParty();
		}
         */

		Application.LoadLevel("Home");
	}

	public void GameClear()
	{
        FindObjectOfType<BGMManager>().StopBGM();
        audioSource.PlayOneShot(gameclearSE);
		clear.SetActive(true);
        createdParty.GetComponent<Party>().SetPlayMode(false);

        if (!isArcade)//アーケードモードの場合は保存しない
        {
            FindObjectOfType<Score>().Save();
            SaveManager sm = FindObjectOfType<SaveManager>();
            sm.arrivedStage = Mathf.Max(stageNumber + 1, sm.arrivedStage);
            createdParty.GetComponent<Party>().SaveParty();
        }
		StartCoroutine("LeaveParty");
	}

	void GameStart()
	{
        //ゲーム開始のSE
        audioSource.PlayOneShot(gamestartSE);

        FindObjectOfType<BGMManager>().SetStageBGM();

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
		title.SetActive(true);
		gameover.SetActive(false);

		if(createdParty == null){
			CreateParty();
		}
		StartCoroutine("AppearParty");


		//createdParty = (GameObject)Instantiate (party, party.transform.position, party.transform.rotation);
	}

    void CreateParty()
    {
        //
        //
        createdParty = (GameObject)Instantiate(party, party.transform.position, party.transform.rotation);

        //
        createdParty.GetComponent<Party>().isUndead = isUndead;
        createdParty.GetComponent<Party>().isArcade = isArcade;
        //

        createdParty.GetComponent<Party>().alexJoin = joinAlex;
        createdParty.GetComponent<Party>().guylusJoin = joinGuylus;
        createdParty.GetComponent<Party>().nelyJoin = joinNely;
        createdParty.GetComponent<Party>().rinmaruJoin = joinRinmaru;
        createdParty.GetComponent<Party>().medhuJoin = joinMedhu;

        createdParty.GetComponent<Party>().SetMember();
    }

	public bool IsPlaying()
	{
		//ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false && clear.activeSelf == false && gameover.activeSelf == false;
	}

	public void GamePause()
    {
		if(paused)
        {
            audioSource.PlayOneShot(tapSE);
            FindObjectOfType<BGMManager>().PlayBGM();
			paused = false;
			pause.SetActive(false);
			Time.timeScale = 1;
		}
		else
        {
            FindObjectOfType<BGMManager>().PauseBGM();

            //SE
            audioSource.PlayOneShot(tapSE);



			paused = true;
			pause.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public void SetPartyForm(int formnum)
    {


		if(createdParty)
        {
			Party.Formation form = Party.Formation.Alex;

            //陣形変更SE
            audioSource.PlayOneShot(tapSE);

            switch(formnum)
            {
			case 0:
				form = Party.Formation.Alex;
				break;
			case 1:
				form = Party.Formation.Guylus;
				break;
			case 2:
				form = Party.Formation.Nely;
				break;
			case 3:
				form = Party.Formation.Rinmaru;
				break;
			case 4:
				form = Party.Formation.Medhu;
				break;
			}
			createdParty.GetComponent<Party>().SetFormation(form);
		}
	}
}
