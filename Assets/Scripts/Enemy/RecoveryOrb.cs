using UnityEngine;
using System.Collections;

public class RecoveryOrb : MonoBehaviour {
	Enemy enemy;
	public GameObject fieldSprite;

	// Use this for initialization
	IEnumerator Start () {
		enemy = GetComponent<Enemy>();

		enemy.Move(new Vector2(Random.Range(-2.0f,2.0f),Random.Range(-2.0f,2.0f)));
		yield return new WaitForSeconds(2.0f);
		enemy.MoveStop();
	}
	
	// Update is called once per frame
	void Update () {
		fieldSprite.transform.Rotate(0,0,Time.deltaTime*30);
	}
}
