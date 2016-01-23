﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour 
{
	public Text stageText;
	public Image stageBack;

	public Transform buttonContent;

	public Sprite back1;
	public Sprite back2;

	int selectnum = 0;

	public GameObject fadeObject;
	Image fadeImage;

	TextAsset tasset;

	int alpha = 0;

	// Use this for initialization
	void Start () 
	{
		SelectStage (0);
	
		fadeImage = fadeObject.GetComponent<Image>();
		fadeObject.SetActive(false);

		SaveManager sm = FindObjectOfType<SaveManager>();
		if(!sm.IsLoaded()){
			sm.Load();
		}
		Transform buttons = GameObject.Find("Content").transform;
		if(sm.arrivedStage < 1){
			setInvisible(buttons,"Stage2");
		}
		if(sm.arrivedStage < 2){
			setInvisible(buttons,"Stage3");
		}
	}
	void setInvisible(Transform buttons,string buttonName){
		Transform b = buttons.FindChild(buttonName);
		b.GetComponent<Button>().interactable = false;
		b.GetComponentInChildren<Text>().text = "";
	}

	void Update()
	{
		if(alpha > 250){
			LoadStage();
		}
	}
	
    void OnGUI()
    {

        if (tapHit() == "ContinueButton")//それぞれのボタンを設定する
        {
			GameStart();
        }
    }
	
	public void GameStart()
	{
		StartCoroutine("FadeStart");
	}

	void LoadStage(){
		switch(selectnum){
		case 0:
			Application.LoadLevel("Stage");
			break;
		default:
			Application.LoadLevel("Stage");
			break;
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

	IEnumerator FadeStart(){
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
		return "Stage" + (num+1).ToString();
	}

	public void SelectStage(int num){	
		//stageBack.sprite =  
		switch(num){
		case 0:
			stageBack.sprite = back1;
			break;
		default:
			stageBack.sprite = back2;
			break;
		}

		string textPath = GetStagePath(num);
		string prevPath = GetStagePath(selectnum);

		buttonContent.FindChild(prevPath).GetComponent<Image>().color = normalButtonColor;
		buttonContent.FindChild(textPath).GetComponent<Image>().color = selectedButtonColor;
		selectnum = num;

		tasset = (TextAsset)Resources.Load(textPath);
		if(tasset){
			stageText.text = tasset.text;
		}
		else{
			stageText.text = "テキストが存在していません。";
		}
	}
}
