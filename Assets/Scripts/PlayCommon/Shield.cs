using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public Player player;

	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c){
		//レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") 
		{
			int damage = 0;
			
			if (layerName == "Bullet(Enemy)")
			{
				Bullet bullet = c.transform.GetComponent<Bullet>();
				damage = bullet.getDamage() / 2;
				if(bullet.isPenetrate == false){
					Destroy(c.gameObject);
				}
			}
			
			if (layerName == "Enemy")
			{
				Enemy enemy = c.transform.GetComponent<Enemy>();
				damage = enemy.getTouchDamage() / 2;
			}
			
			player.damageHP(damage);
		}
	}
}
