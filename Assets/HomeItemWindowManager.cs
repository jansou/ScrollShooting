using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeItemWindowManager : MonoBehaviour {
	
	public int selectNumber = 1;
	public Text numText;
	public Text priceText;
	public Text hasMoneyText;
	public Text explanationText;
	public int selectPrice = 0;
	public ItemType selectItem = ItemType.None;
	int hasMoney = 0;
	int oneItemPrice = 0;

	bool somethingBought = false;//何かが買われたかどうか

	public GameObject hasItemCanvas;
	public GameObject shopCanvas;
	// Use this for initialization
	void Start () {
		numText.text = selectNumber.ToString();
		priceText.text = "0 G";

		hasItemCanvas.SetActive(false);
		shopCanvas.SetActive(false);
		ChangeWindowShop();
	}

	public void InitWindow(){
		numText.text = selectNumber.ToString();
		priceText.text = "0 G";

		SaveManager sm = FindObjectOfType<SaveManager>();
		hasMoney = sm.money;
		hasMoneyText.text = hasMoney.ToString() + " G";
	}

	public void ChangeWindowHasItem(){
		if(hasItemCanvas.activeSelf == false){
			shopCanvas.SetActive(false);
			hasItemCanvas.SetActive(true);
			explanationText.text = "現在持っているアイテムです";
			if(somethingBought){
				//HasItem担当のItemWindowManagerのパネルを再生成
				ItemWindowManager[] items = GetComponentsInChildren<ItemWindowManager>();
				for(int i=0; i<items.Length; ++i){
					if(items[i].displayType == ItemWindowManager.DisplayType.HasItem){
						items[i].RecreatePanel();
						break;
					}
				}
				somethingBought = false;
			}
		}
	}
	public void ChangeWindowShop(){
		if(shopCanvas.activeSelf == false){
			hasItemCanvas.SetActive(false);
			shopCanvas.SetActive(true);
			explanationText.text = "買うアイテムを選んでください";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//決済
	public void BuyItem(){
		//何も選択していない
		if(selectItem == ItemType.None){
			return;
		}

		int needMoney = oneItemPrice * selectNumber;
		if(hasMoney >= needMoney){
			somethingBought = true;
			explanationText.text = "ありがとうございました！";
			hasMoney -= needMoney;
			hasMoneyText.text = hasMoney.ToString() + " G";

			SaveManager sm = FindObjectOfType<SaveManager>();
			switch(selectItem){
			case ItemType.NormalHerb:
				sm.itemNumInfo.normalHerb += selectNumber;
				break;
			case ItemType.NiceHerb:
				sm.itemNumInfo.niceHerb += selectNumber;
				break;
			case ItemType.GreatHerb:
				sm.itemNumInfo.greatHerb += selectNumber;
				break;
			case ItemType.LifeOrb:
				sm.itemNumInfo.lifeOrb += selectNumber;
				break;
			case ItemType.GreatLifeOrb:
				sm.itemNumInfo.greatLifeOrb += selectNumber;
				break;
			}
			sm.money = hasMoney;
			sm.SaveData();
		}
		else{
			explanationText.text = "ゴールドがたりません";
		}
	}

	public void ChangeCounter(int n){
		selectNumber = Mathf.Max (1,selectNumber + n);
		numText.text = selectNumber.ToString();
		WritePrice();
	}
	public void ChangeItem(ItemData data){
		oneItemPrice = data.price;
		selectItem = data.type;
		WritePrice();
	}

	void WritePrice(){
		selectPrice = oneItemPrice * selectNumber;
		priceText.text = selectPrice.ToString() + " G";
	}
}
