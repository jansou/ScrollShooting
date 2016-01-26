using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour {
	public GameObject explosion;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroy(){
		if(GetComponent<Enemy>().hp > 0){
			return;
		}
		//delete enemy bullet
		GameObject[] enemyBullets;
		enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
		foreach (GameObject enemyBullet in enemyBullets)
		{
			Destroy(enemyBullet);
		}
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			if(enemy.name != this.name){
				Enemy e = enemy.GetComponent<Enemy>();
				if(e){
					e.ExplodeSelf();
				}
			}
		}


		Instantiate (explosion,transform.position+new Vector3(0.5f,0,0),transform.rotation);
		Instantiate (explosion,transform.position+new Vector3(-0.5f,0,0),transform.rotation);
		Instantiate (explosion,transform.position+new Vector3(0,-0.5f,0),transform.rotation);
		Instantiate (explosion,transform.position+new Vector3(0,0.5f,0),transform.rotation);
	}
}
