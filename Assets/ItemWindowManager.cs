using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemWindowManager : MonoBehaviour {
	public GameObject panel;
	public Transform content;
	public Text bottomText;

	public ItemType selectType = ItemType.None;
	// Use this for initialization
	void Start () {
		GameObject o = Resources.Load("niceherb") as GameObject;
		CreatePanelByData(o.GetComponent<ItemData>());
		o = Resources.Load("normalherb") as GameObject;
		CreatePanelByData(o.GetComponent<ItemData>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetPanel(){
		//ボタンの色を設定
		for(int i=0; i<content.childCount; ++i){
			ColorBlock cb = content.GetChild(i).GetComponent<Button>().colors;
			cb.normalColor = new Color32(0,0,0,0);
			cb.highlightedColor = new Color32(0,0,0,0);
			content.GetChild(i).GetComponent<Button>().colors = cb;
		}
		selectType = ItemType.None;

		bottomText.text = "アイテムを選んでください";
	}

	public void CreatePanelByData(ItemData data){
		GameObject o = Instantiate(panel);
		Transform t = o.transform;
		t.FindChild("NameText").GetComponent<Text>().text = data.itemname;
		t.FindChild("NumberText").GetComponent<Text>().text = "X1";
		t.FindChild("Image").GetComponent<Image>().sprite = data.image;
		t.SetParent(content);
		t.localScale = new Vector3(1,1,1);

		o.GetComponent<Button>().onClick.AddListener(() => {
			selectType = data.type;
			bottomText.text = data.explanation;

			//ボタンの色を設定
			for(int i=0; i<content.childCount; ++i){
				ColorBlock cb = content.GetChild(i).GetComponent<Button>().colors;
				cb.normalColor = new Color32(0,0,0,0);
				cb.highlightedColor = new Color32(0,0,0,0);
				content.GetChild(i).GetComponent<Button>().colors = cb;
			}

			ColorBlock cblock = o.GetComponent<Button>().colors;
			cblock.normalColor = Color.white;
			cblock.highlightedColor = Color.white;
			o.GetComponent<Button>().colors = cblock;
		});
	}
}
