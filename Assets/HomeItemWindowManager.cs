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
	int hasMoney = 0;
	int oneItemPrice = 0;

	public GameObject hasItemCanvas;
	public GameObject shopCanvas;
	// Use this for initialization
	void Start () {
		numText.text = selectNumber.ToString();
		priceText.text = "0 G";
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
		shopCanvas.SetActive(false);
		hasItemCanvas.SetActive(true);
		explanationText.text = "現在持っているアイテムです";
	}
	public void ChangeWindowShop(){
		hasItemCanvas.SetActive(false);
		shopCanvas.SetActive(true);
		explanationText.text = "買うアイテムを選んでください";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//決済
	public void BuyItem(){
		int needMoney = oneItemPrice * selectNumber;
		if(hasMoney >= needMoney){
			explanationText.text = "ありがとうございました！";
			hasMoney -= needMoney;
			hasMoneyText.text = hasMoney.ToString() + " G";
		}
		else{
			explanationText.text = "ゴールドがたりません";
		}
	}

	public void changeCounter(int n){
		selectNumber = Mathf.Max (1,selectNumber + n);
		numText.text = selectNumber.ToString();
		WritePrice();
	}
	public void changePrice(int price){
		oneItemPrice = price;
		WritePrice();
	}

	void WritePrice(){
		selectPrice = oneItemPrice * selectNumber;
		priceText.text = selectPrice.ToString() + " G";
	}
}
