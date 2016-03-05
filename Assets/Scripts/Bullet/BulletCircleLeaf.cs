using UnityEngine;
using System.Collections;

public class BulletCircleLeaf : MonoBehaviour {
	public GameObject leafBullet;
	// Use this for initialization

    public int power = 2;

	void Start () {
		for(int i=0; i<10; ++i){
			GameObject o = (GameObject)Instantiate (leafBullet);
            o.GetComponent<Bullet>().shotPower = power;
			o.transform.SetParent(transform);
			float a = i*Mathf.PI*2/10;
			o.transform.localPosition = new Vector3(Mathf.Cos(a),Mathf.Sin(a),0)*0.3f;
			o.transform.localRotation = Quaternion.Euler(0,0,36*i);
			o.GetComponent<Bullet>().speed = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.childCount == 0){
			Destroy (this.gameObject);
		}
		transform.Translate(0,2f*Time.deltaTime,0);
		//transform.Rotate(0,0,5);
	}
}
