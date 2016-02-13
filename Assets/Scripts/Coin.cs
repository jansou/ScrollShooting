using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public int point = 10;
	public Transform sprite;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-Time.deltaTime*1,0,0);

		sprite.Rotate(0,Time.deltaTime*150,0);
	}
}
