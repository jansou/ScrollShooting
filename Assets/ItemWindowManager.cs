using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemWindowManager : MonoBehaviour {
	public GameObject panel;
	public Transform content;
	public Text bottomText;

	public ItemType selectType = ItemType.None;

	public enum DisplayType{
		HasItem,
		Shop
	}
	public DisplayType displayType = DisplayType.HasItem;

	HomeItemWindowManager homeManager;
	// Use this for initialization
	void Start () {
		//持ち物
		if(displayType == DisplayType.HasItem){
			CreateByItemName("normalherb");
		}
		//お店
		else{
			CreateByItemName("normalherb");
			CreateByItemName("niceherb");
			CreateByItemName("greatherb");
		}

		//ホーム用
		homeManager = FindObjectOfType<HomeItemWindowManager>();
	}

	void CreateByItemName(string itemName){
		GameObject o = Resources.Load(itemName) as GameObject;
		CreatePanelByData(o.GetComponent<ItemData>());
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
	
	public void CreatePanelByData(ItemData data){
		GameObject o = CreatePanel(data);

		o.GetComponent<Button>().onClick.AddListener(() => {
			selectType = data.type;
			bottomText.text = data.explanation;

			//ホーム画面なら
			if(homeManager){
				homeManager.changePrice(data.price);
			}

			ResetColor();

			ColorBlock cblock = o.GetComponent<Button>().colors;
			cblock.normalColor = Color.white;
			cblock.highlightedColor = Color.white;
			o.GetComponent<Button>().colors = cblock;
		});
	}
	GameObject CreatePanel(ItemData data){
		GameObject o = Instantiate(panel);
		Transform t = o.transform;
		t.FindChild("NameText").GetComponent<Text>().text = data.itemname;
		t.FindChild("NumberText").GetComponent<Text>().text = "X1";
		t.FindChild("Image").GetComponent<Image>().sprite = data.image;
		t.SetParent(content);
		t.localScale = new Vector3(1,1,1);
		return o;
	}
}
