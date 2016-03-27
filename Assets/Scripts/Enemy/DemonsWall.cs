using UnityEngine;
using System.Collections;

public class DemonsWall : MonoBehaviour {
	Enemy enemy;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
		enemy.Move(Vector3.right * 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
