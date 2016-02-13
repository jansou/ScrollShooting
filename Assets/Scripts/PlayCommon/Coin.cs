using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public int point = 1;
	public Transform sprite;
    public float speed=0.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
		transform.Translate(-Time.deltaTime*speed,0,0);

		sprite.Rotate(0,Time.deltaTime*150,0);
	}
}
