using UnityEngine;
using System.Collections;

public class EnemyCommon : MonoBehaviour {

	Transform shotPositions;

	// Use this for initialization
	void Start () {
	}

	public void Init(){
		GameObject oo = new GameObject();
		oo.name = "shotPositions";
		oo.transform.parent = transform;
		oo.transform.localPosition = new Vector3(0,0,0);
		shotPositions = oo.transform;
	}

	public Transform CreateShotPosition(){
		GameObject o = (GameObject)Instantiate(Resources.Load("ShotPosition"));
		o.transform.parent = shotPositions;
		o.transform.localPosition = new Vector3(0,0,0);
		return o.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
