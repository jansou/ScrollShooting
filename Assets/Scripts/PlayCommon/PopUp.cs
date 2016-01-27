using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {
	public Canvas drawCanvas;
	public GameObject textObj;
	public GameObject levelUpObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CreateText(Vector3 position,string str){
		GameObject o = (GameObject)Instantiate(textObj);
		o.transform.position = Camera.main.WorldToScreenPoint(position);
		o.transform.SetParent(drawCanvas.transform);
		
		o.GetComponent<Text>().text = str;
	}
	public void CreateLevelUpText(Vector3 position){
		GameObject o = (GameObject)Instantiate(levelUpObj);
		o.transform.position = Camera.main.WorldToScreenPoint(position);
		o.transform.SetParent(drawCanvas.transform);
	}

	public void CreateText(Vector3 position,int num){
		CreateText(position,num.ToString());
	}
}
