using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpTest : MonoBehaviour {
	public GameObject monPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			FindObjectOfType<PopUp>().CreateText(monPos.transform.position,Random.Range(10,1000));
		}
	}
}
