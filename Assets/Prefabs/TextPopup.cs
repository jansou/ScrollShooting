using UnityEngine;
using System.Collections;

public class TextPopup : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,Time.deltaTime*40,0);
	}
}
