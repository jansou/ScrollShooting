﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemWindowManager : MonoBehaviour {
	public GameObject panel;
	public Transform content;
	public Text bottomText;

	public ItemType selectType = ItemType.None;

	public enum DisplayType{
		HasItem,
		Shop,
		EX
	}
	public DisplayType displayType = DisplayType.HasItem;

	HomeItemWindowManager homeManager;
	Manager mainManager;
	// Use this for initialization
	void Start () {
		//ホーム用
		homeManager = FindObjectOfType<HomeItemWindowManager>();
		//メイン用
		mainManager = FindObjectOfType<Manager>();

		CreatePanels();
	}

	void CreatePanels(){
		//持ち物
		if (displayType == DisplayType.HasItem) {
			SaveManager sm = FindObjectOfType<SaveManager> ();
			SaveManager.ItemNumInfo items = sm.itemNumInfo;
			if (items.normalHerb > 0) {
				CreateByItemName ("normalherb", items.normalHerb);
			}
			if (items.niceHerb > 0) {
				CreateByItemName ("niceherb", items.niceHerb);
			}
			if (items.greatHerb > 0) {
				CreateByItemName ("greatherb", items.greatHerb);
			}
			if (items.lifeOrb > 0) {
				CreateByItemName ("lifeorb", items.lifeOrb);
			}
			if (items.greatLifeOrb > 0) {
				CreateByItemName ("greatlifeorb", items.greatLifeOrb);
			}
		} else if (displayType == DisplayType.EX) {

			EXItem exItem = FindObjectOfType<EXItem>();
			if (exItem.normalHerbNum > 0) {
				CreateByItemName ("normalherb", exItem.normalHerbNum);
			}
			if (exItem.niceHerbNum > 0) {
				CreateByItemName ("niceherb", exItem.niceHerbNum);
			}
			if (exItem.greatHerbNum > 0) {
				CreateByItemName ("greatherb", exItem.greatHerbNum);
			}
			if (exItem.lifeOrbNum > 0) {
				CreateByItemName ("lifeorb", exItem.lifeOrbNum);
			}
			if (exItem.greatLifeOrbNum > 0) {
				CreateByItemName ("greatlifeorb", exItem.greatLifeOrbNum);
			}
		}
		//お店
		else{
			CreateByItemName("normalherb",0);
			CreateByItemName("niceherb",0);
			CreateByItemName("greatherb",0);
			CreateByItemName("lifeorb",0);
			CreateByItemName("greatlifeorb",0);
		}
	}

	//持ち物が増減した時にパネルを再生成
	public void RecreatePanel(){
		for(int i=0; i<content.childCount; ++i){
			Destroy(content.GetChild(i).gameObject);
		}
		CreatePanels();
	}

	void CreateByItemName(string itemName,int num){
		GameObject o = Resources.Load(itemName) as GameObject;
		CreatePanelByData(o.GetComponent<ItemData>(),num);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetPanel(){
		ResetColor();
		selectType = ItemType.None;

		bottomText.text = "アイテムを選んでください";
	}

	void ResetColor(){
		//ボタンの色を設定
		for(int i=0; i<content.childCount; ++i){
			ColorBlock cb = content.GetChild(i).GetComponent<Button>().colors;
			cb.normalColor = new Color32(0,0,0,0);
			cb.highlightedColor = new Color32(0,0,0,0);
			content.GetChild(i).GetComponent<Button>().colors = cb;
		}
	}
	
	public void CreatePanelByData(ItemData data,int num){
		GameObject o = CreatePanel(data,num);

		o.GetComponent<Button>().onClick.AddListener(() => {
			selectType = data.type;
			bottomText.text = data.explanation;

			//ホーム画面なら
			if(homeManager){
				homeManager.ChangeItem(data);
			}
			if(mainManager){
				switch(data.type){
				case ItemType.GreatLifeOrb:
					mainManager.NotifyUseAll();
					break;
				default:
					mainManager.NotifyUseOne();
					break;
				}
			}

			ResetColor();

			ColorBlock cblock = o.GetComponent<Button>().colors;
			cblock.normalColor = Color.white;
			cblock.highlightedColor = Color.white;
			o.GetComponent<Button>().colors = cblock;
		});
	}
	GameObject CreatePanel(ItemData data,int num){
		GameObject o = Instantiate(panel);
		Transform t = o.transform;
		t.FindChild("NameText").GetComponent<Text>().text = data.itemname;
		string numstr = num > 0 ? "X" + num.ToString() : "";
		t.FindChild("NumberText").GetComponent<Text>().text = numstr;
		t.FindChild("Image").GetComponent<Image>().sprite = data.image;
		t.SetParent(content);
		t.localScale = new Vector3(1,1,1);
		return o;
	}
}
