using UnityEngine;
using System.Collections;

public class TextPopup : MonoBehaviour {
	public float deleteTime = 0.5f;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(deleteTime);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,Time.deltaTime*40,0);
	}
}
