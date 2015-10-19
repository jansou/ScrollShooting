using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FormationButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(()=>{
			FindObjectOfType<Party>().ChangeFormation();
		});
	}
	
	// Update is called once per frame
	void Update () {

	}
}
