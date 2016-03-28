using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour 
{
	public Text stageText;
	public Image stageBack;

	public Transform buttonContent;

    public Sprite back0;
    public Sprite back1;
    public Sprite back2;
	public Sprite back3;
	public Sprite backEX;
    public Sprite backCredit;

    //おまけステージの番号
    public int EXstageNum=9;
    public int creditNum = 5;

	int selectStageButtonNum = 0;
    string selectTargetStageName = "Stage00";
    //Fade関連
    int alpha = 255;
	public GameObject fadeObject;
	Image fadeImage;
    private int state = 0;

	TextAsset tasset;

    public AudioClip tapSE;
    public AudioClip startSE;
    public AudioClip BGM;
    AudioSource audioSource;

	bool firstSelect = true;// for SelectStage's sound

	public Canvas itemShopCanvas;

	// Use this for initialization
	void Start () 
	{
		itemShopCanvas.enabled = false;

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
        //

		SelectStage (0);
	    
        //Fade関係
		fadeImage = fadeObject.GetComponent<Image>();
		fadeObject.SetActive(false);

		SaveManager sm = FindObjectOfType<SaveManager>();
		if(!sm.IsLoaded()){
			sm.Load();
		}
		Transform buttons = GameObject.Find("Content").transform;

        for (int i = 2; sm.arrivedStage < i;++i )
        {
            setInvisible(buttons, "Stage"+i.ToString());
        }



        /*

            if (sm.arrivedStage < 2)
            {
                setInvisible(buttons, "Stage2");
            }
		if(sm.arrivedStage < 3){
			setInvisible(buttons,"Stage3");
		}....
        */
        if (sm.arrivedStage < EXstageNum)
        {
            setInvisible(buttons, "StageEX");
        }
        
        StartCoroutine("FadeIn");
	}

	void setInvisible(Transform buttons,string buttonName){
		Transform b = buttons.FindChild(buttonName);
		b.GetComponent<Button>().interactable = false;
		b.GetComponentInChildren<Text>().text = "";
	}

	void Update()
	{
        if (alpha > 250 && state != 0)
        {
			LoadStage();
		}

		if(Input.GetKeyDown(KeyCode.Escape) && Application.platform == RuntimePlatform.Android){
			ReturnTitle();
		}
	}

    IEnumerator FadeIn()
    {
        while (true)
        {
            if (state == 0)
            {
                fadeObject.SetActive(true);

                if (alpha < 5)
                {
                    state += 1;
                    fadeObject.SetActive(false);
                    yield return null;
                }

                fadeImage.color = new Color(1, 1, 1, alpha / 255.0f);
                alpha -= 5;
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }
	
    void OnGUI()
    {

        if (tapHit() == "ContinueButton" && state != 0 && itemShopCanvas.enabled == false)//それぞれのボタンを設定する
        {
            selectStageButtonNum = FindObjectOfType<SaveManager>().arrivedStage;
			GameStart();
        }
    }
	
	public void GotoShop(){
		itemShopCanvas.enabled = true;
		FindObjectOfType<HomeItemWindowManager>().InitWindow();
	}
	public void ReturnFromShop(){
		itemShopCanvas.enabled = false;
	}

	public void ReturnTitle()
	{
		audioSource.PlayOneShot(tapSE);
		StartCoroutine("FadeReturn");
		//Application.LoadLevel("Title");
	}

	IEnumerator FadeReturn(){
		fadeObject.SetActive(true);
		while(alpha < 250){
			fadeImage.color = new Color(1,1,1,alpha/255.0f);
			alpha = Mathf.Min(250,alpha+7);
			yield return new WaitForEndOfFrame();
		}
		Application.LoadLevel("Title");
	}
	
	public void GameStart()
	{
        audioSource.Stop();
        audioSource.PlayOneShot(startSE);
		StartCoroutine("FadeStart");
	}

	void LoadStage(){

        if (EXstageNum == selectStageButtonNum)
        {
            Application.LoadLevel("StageEX");

        }
        else
        {
            Application.LoadLevel(selectTargetStageName);
            /*
            switch (selectStageButtonNum)
            {
                case 0:

                    Application.LoadLevel("Stage0" + (selectTargetStageNum).ToString());
                    //Application.LoadLevel("Stage01");
                    break;
                case 1:
                    Application.LoadLevel("Stage0" + (selectStageButtonNum).ToString());
                    //Application.LoadLevel("Stage02");
                    break;
                case 2:
                    Application.LoadLevel("Stage0" + (selectStageButtonNum).ToString());
                    //Application.LoadLevel("Stage03");
                    break;
                case 3:
                    Application.LoadLevel("Stage0" + (selectStageButtonNum).ToString());
                    //Application.LoadLevel("Stage03");
                    break;
                default:
                    Application.LoadLevel("Stage");
                    break;
             
            }
             */
        }
	}

    string tapHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
                if (hitObject)
                {

                    
                    return hitObject.collider.gameObject.name;
                }
            }
        }
        return "none";
    }

	IEnumerator FadeStart()
    {
		fadeObject.SetActive(true);
		while(true){
			fadeImage.color = new Color(1,1,1,alpha/255.0f);
			alpha += 5;
			yield return new WaitForEndOfFrame();
		}
	}

	public Color normalButtonColor;
	public Color selectedButtonColor;

	string GetStagePath(int num){
		/*
		switch(num){
		case 0:
			return "Stage1";
		default:
			return "Stage2";
		}
	*/
		//return "Stage" + (num+1).ToString();
        if (num == EXstageNum)
        {
            return "StageEX";
        }
        if (num == creditNum)
        {
            return "Credit";
        }

        return "Stage" + (num).ToString();
	}

    public void SelectTargetStage(string name)
    {
        selectTargetStageName = name;
    }

	public void SelectStage(int num)
    {
		if(firstSelect){
			firstSelect = false;
		}
		else{
        	audioSource.PlayOneShot(tapSE);
		}
		//stageBack.sprite =  

        if (EXstageNum == num)
        {
            stageBack.sprite = backEX;
        }
        else if (creditNum == num)
        {
            stageBack.sprite = backCredit;
        }
        else
        {
            switch (num)
            {
                case 0:
                    stageBack.sprite = back0;
                    break;
                case 1:
                    stageBack.sprite = back1;
                    break;
                case 2:
                    stageBack.sprite = back2;
                    break;
                case 3:
                    stageBack.sprite = back3;
                    break;
                default:
                    stageBack.sprite = back1;
                    break;
            }
        }

		string textPath = GetStagePath(num);
		string prevPath = GetStagePath(selectStageButtonNum);

		buttonContent.FindChild(prevPath).GetComponent<Image>().color = normalButtonColor;
		buttonContent.FindChild(textPath).GetComponent<Image>().color = selectedButtonColor;
		selectStageButtonNum = num;

		if(num == creditNum){
			GameObject.Find("startButton").GetComponent<Button>().interactable = false;
		}
		else{
			GameObject.Find("startButton").GetComponent<Button>().interactable = true;
		}

		tasset = (TextAsset)Resources.Load(textPath);
		if(tasset){
			stageText.text = tasset.text;
		}
		else{
			stageText.text = "テキストが存在していません。";
		}
	}
}
