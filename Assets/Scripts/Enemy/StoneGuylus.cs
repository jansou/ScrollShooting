using UnityEngine;
using System.Collections;

public class StoneGuylus : MonoBehaviour {
	public GameObject shieldField;
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;
	
	// Use this for initialization
	IEnumerator  Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();
		
		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;
		
		yield return new WaitForSeconds(2.0f);
		
		StartCoroutine("Attack");
	}
	
	IEnumerator Attack(){
		while(true){
			while(shieldField.transform.localScale.x < 1.0f){
				Vector3 s = shieldField.transform.localScale;
				float t = Time.deltaTime;
				s.x += t;
				s.y += t;
				s.z += t;
				shieldField.transform.localScale = s;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(5.0f);
			while(shieldField.transform.localScale.x > 0.0f){
				Vector3 s = shieldField.transform.localScale;
				float t = Time.deltaTime;
				s.x -= t;
				s.y -= t;
				s.z -= t;
				shieldField.transform.localScale = s;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
