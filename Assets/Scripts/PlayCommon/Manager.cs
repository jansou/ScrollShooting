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

	private GameObject itemWindow;

    //闘技場用の設定を行うか
    public bool isArcade=false;

    //プレイモードでないならプレイヤーは弾が撃てない
    public bool isPlayMode = true;

	bool paused = false;
	bool usingItem = false;

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
	public AudioClip recoverySE;
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
		itemWindow = GameObject.Find("ItemCanvas");

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

		itemWindow.SetActive(false);

		GameStart();

        //Debug.Log("[Party.cs] Stop→" + b);
        //createdParty.GetComponent<Party>().SetPlayMode(isPlayMode);
	}
	IEnumerator AppearParty(){
		int f = 0;
		while(f < 60){
			createdParty.transform.Translate(0.03f,0,0);
			++f;
			yield return new WaitForEndOfFrame();
		}
		title.SetActive(false);
		createdParty.GetComponent<Party>().SetPlayMode(isPlayMode);
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
        audioSource.Stop();
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

	void DeleteTagObjects(string tagName)
    {
		GameObject[] os;
		os = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject o in os)
		{
			Destroy(o);
		}
	}

	void GameStart()
	{
        //ゲーム開始のSE
        audioSource.PlayOneShot(gamestartSE);

        FindObjectOfType<BGMManager>().SetStageBGM();

		DeleteTagObjects("EnemyBullet");
		DeleteTagObjects("PlayerBullet");
		DeleteTagObjects("Enemy");
		DeleteTagObjects("Coin");
		FindObjectOfType<Emitter>().ResetWave();

        //ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
		title.SetActive(true);

		if(createdParty == null)
        {
			CreateParty();
		}

		StartCoroutine("AppearParty");
	}

    void CreateParty()
    {
        createdParty = (GameObject)Instantiate(party, party.transform.position, party.transform.rotation);

		Party p = createdParty.GetComponent<Party>();

        p.isUndead = isUndead;
		p.isArcade = isArcade;
		p.alexJoin = joinAlex;
		p.guylusJoin = joinGuylus;
		p.nelyJoin = joinNely;
		p.rinmaruJoin = joinRinmaru;
		p.medhuJoin = joinMedhu;

		p.SetMember();

		SetPartyForm(0);

        gameover.SetActive(false);
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

	//全体使用のテイテム時にItemWindowManagerから呼び出す
	public void NotifyUseAll(){
		selectnum = 5;
		Transform playerUIs = GameObject.Find("Players").transform;
		playerUIs.FindChild("1stPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = true;
		playerUIs.FindChild("2ndPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = true;
		playerUIs.FindChild("3rdPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = true;
		playerUIs.FindChild("4thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = true;
		playerUIs.FindChild("5thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = true;
	}
	public void NotifyUseOne(){
		if(selectnum == 5){
			Transform playerUIs = GameObject.Find("Players").transform;
			playerUIs.FindChild("1stPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("2ndPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("3rdPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("4thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("5thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			selectnum = -1;
		}
	}

	public void GameUseItem()
	{
		//スタート中,クリア中なら何もしない
		if(title.activeSelf == true || clear.activeSelf == true){
			return;
		}

		if(usingItem)
		{
			Transform playerUIs = GameObject.Find("Players").transform;
			playerUIs.FindChild("1stPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("2ndPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("3rdPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("4thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			playerUIs.FindChild("5thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
			selectnum = -1;
			FindObjectOfType<ItemWindowManager>().ResetPanel();

			playerUIs.FindChild("1stPlayer").GetComponent<Button>().interactable = alexButtonInteractable;
			playerUIs.FindChild("2ndPlayer").GetComponent<Button>().interactable = guylusButtonInteractable;
			playerUIs.FindChild("3rdPlayer").GetComponent<Button>().interactable = nelyButtonInteractable;
			playerUIs.FindChild("4thPlayer").GetComponent<Button>().interactable = rinmaruButtonInteractable;
			playerUIs.FindChild("5thPlayer").GetComponent<Button>().interactable = medhuButtonInteractable;

			//メインに戻る
			Time.timeScale = 1;
			itemWindow.SetActive(false);
			usingItem = false;
		}
		else
		{
			//アイテム使用画面へ
			Time.timeScale = 0;
			itemWindow.SetActive(true);
			usingItem = true;

			Transform playerUIs = GameObject.Find("Players").transform;
			alexButtonInteractable = playerUIs.FindChild("1stPlayer").GetComponent<Button>().interactable;
			guylusButtonInteractable = playerUIs.FindChild("2ndPlayer").GetComponent<Button>().interactable;
			nelyButtonInteractable = playerUIs.FindChild("3rdPlayer").GetComponent<Button>().interactable;
			rinmaruButtonInteractable = playerUIs.FindChild("4thPlayer").GetComponent<Button>().interactable;
			medhuButtonInteractable = playerUIs.FindChild("5thPlayer").GetComponent<Button>().interactable;

			playerUIs.FindChild("1stPlayer").GetComponent<Button>().interactable = true;
			playerUIs.FindChild("2ndPlayer").GetComponent<Button>().interactable = true;
			playerUIs.FindChild("3rdPlayer").GetComponent<Button>().interactable = true;
			playerUIs.FindChild("4thPlayer").GetComponent<Button>().interactable = true;
			playerUIs.FindChild("5thPlayer").GetComponent<Button>().interactable = true;

		}
	}

	void SetMark(Transform t,string UIName,bool b){
		GameObject.Find("Players").transform.FindChild(UIName).FindChild("LeaderMark").GetComponent<Image>().enabled = b;
	}

	bool alexButtonInteractable;
	bool guylusButtonInteractable;
	bool nelyButtonInteractable;
	bool rinmaruButtonInteractable;
	bool medhuButtonInteractable;
	
	int selectnum = -1; // -1..選択していない, 5..全体
	public void SelectCharacter(int num){
		if(usingItem){
			//同キャラが２回押された
			if(selectnum == num || selectnum == 5){
				ItemWindowManager iwm = FindObjectOfType<ItemWindowManager>();
				ItemType type = iwm.selectType;

				Party p = createdParty.GetComponent<Party>();

				if(type != ItemType.None && p.CanUseItem(type,num)){
					switch(type){
					case ItemType.NormalHerb:
					case ItemType.NiceHerb:
					case ItemType.GreatHerb:
						p.UseItemToMember(type,num);
						break;
					case ItemType.LifeOrb:
						p.UseReviveItemToMember(type,num);
						break;
					case ItemType.GreatLifeOrb:
						p.UseReviveItemToAll();
						break;
					default:
						FindObjectOfType<MessageWindow>().showMessage("まだ存在しないアイテムです。");
						break;
					}

					ItemWindowManager.DisplayType displayType = iwm.displayType;

					if (displayType == ItemWindowManager.DisplayType.EX) {
						EXItem exItem = FindObjectOfType<EXItem>();
						switch(type){
						case ItemType.NormalHerb:
							--exItem.normalHerbNum;
							break;
						case ItemType.NiceHerb:
							--exItem.niceHerbNum;
							break;
						case ItemType.GreatHerb:
							--exItem.greatHerbNum;
							break;
						case ItemType.LifeOrb:
							--exItem.lifeOrbNum;
							break;
						case ItemType.GreatLifeOrb:
							--exItem.greatLifeOrbNum;
							break;
						}
					}
					else {
						SaveManager sm = FindObjectOfType<SaveManager>();
						switch(type){
						case ItemType.NormalHerb:
							--sm.itemNumInfo.normalHerb;
							break;
						case ItemType.NiceHerb:
							--sm.itemNumInfo.niceHerb;
							break;
						case ItemType.GreatHerb:
							--sm.itemNumInfo.greatHerb;
							break;
						case ItemType.LifeOrb:
							--sm.itemNumInfo.lifeOrb;
							break;
						case ItemType.GreatLifeOrb:
							--sm.itemNumInfo.greatLifeOrb;
							break;
						}
					}

					audioSource.PlayOneShot(recoverySE);

					iwm.RecreatePanel();

					Time.timeScale = 1;
					itemWindow.SetActive(false);
					usingItem = false;

					iwm.ResetPanel();
					selectnum = -1;

					Transform playerUIs = GameObject.Find("Players").transform;
					playerUIs.FindChild("1stPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
					playerUIs.FindChild("2ndPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
					playerUIs.FindChild("3rdPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
					playerUIs.FindChild("4thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
					playerUIs.FindChild("5thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = false;
				}
			}
			else{
				selectnum = num;
				Transform playerUIs = GameObject.Find("Players").transform;
				if(joinAlex){
					playerUIs.FindChild("1stPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = (num == 0);
				}
				if(joinGuylus){
					playerUIs.FindChild("2ndPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = (num == 1);
				}
				if(joinNely){
					playerUIs.FindChild("3rdPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = (num == 2);
				}
				if(joinRinmaru){
					playerUIs.FindChild("4thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = (num == 3);
				}
				if(joinMedhu){
					playerUIs.FindChild("5thPlayer").FindChild("whiteImage").GetComponent<Image>().enabled = (num == 4);
				}
			}
		}
		else{
			SetPartyForm(num);
		}
	}

	private bool firstFormChange = true;

	public void SetPartyForm(int formnum)
    {
		Transform t = GameObject.Find("Players").transform;
		SetMark (t,"1stPlayer",false);
		SetMark (t,"2ndPlayer",false);
		SetMark (t,"3rdPlayer",false);
		SetMark (t,"4thPlayer",false);
		SetMark (t,"5thPlayer",false);

		if(createdParty)
        {
			Party.Formation form = Party.Formation.Alex;
			string uiName = "1stPlayer";

            //陣形変更SE
			if (firstFormChange){
				firstFormChange = false;
			}
			else{
	            audioSource.PlayOneShot(tapSE);
			}

            switch(formnum)
            {
			case 0:
				form = Party.Formation.Alex;
				uiName = "1stPlayer";
				break;
			case 1:
				form = Party.Formation.Guylus;
				uiName = "2ndPlayer";
				break;
			case 2:
				form = Party.Formation.Nely;
				uiName = "3rdPlayer";
				break;
			case 3:
				form = Party.Formation.Rinmaru;
				uiName = "4thPlayer";
				break;
			case 4:
				form = Party.Formation.Medhu;
				uiName = "5thPlayer";
				break;
			}
			SetMark (t,uiName,true);
			createdParty.GetComponent<Party>().SetFormation(form);
		}
	}
}
