using UnityEngine;
using System.Collections;

public class EnemyHP_Render : MonoBehaviour 
{
    Enemy enemy;

    float maxhp=0;
    float hp = 0;

	// Use this for initialization
	void Start () 
    {
        enemy = transform.parent.parent.GetComponent<Enemy>();

        maxhp = enemy.hp;
        hp = maxhp;
        if(maxhp<=0)
        {
            Debug.Log("不正な体力値です");
        }

        
	}
	
	// Update is called once per frame
	void Update () 
    {
        hp = enemy.hp;

        transform.localScale = new Vector3(hp / maxhp,1,1);
	}
}
