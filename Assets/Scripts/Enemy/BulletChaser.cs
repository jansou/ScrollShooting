using UnityEngine;
using System.Collections;

public class BulletChaser : MonoBehaviour {
	Transform pt;
	Rigidbody2D rigid;
	float spd;
	// Use this for initialization
	void Start () {
		pt = FindObjectOfType<Party>().transform;
		rigid = GetComponent<Rigidbody2D>();
		spd = GetComponent<Bullet>().speed;

		StartCoroutine ("Chase");
	}

	IEnumerator Chase(){
		yield return new WaitForSeconds(0.4f);
		while(spd > 0.3f){
			spd -= 0.1f;
			rigid.velocity = transform.up.normalized*spd;
			yield return new WaitForEndOfFrame();
		}
		if(pt){
			Vector3 v = pt.position - transform.position;
			float rr = 0.5f;
			v += new Vector3(Random.Range (-rr,rr),Random.Range(-rr,rr));
			transform.localRotation = Quaternion.FromToRotation(Vector3.up,v);
			rigid.velocity = transform.up.normalized*spd;
		}
		while(spd < 5f){
			spd += 0.5f;
			rigid.velocity = transform.up.normalized*spd;
			yield return new WaitForEndOfFrame();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
