using UnityEngine;
using System.Collections;

public class PlayerHP_Render : MonoBehaviour 
{
    //Player player;

    float maxhp=0;
    float hp = 0;

	// Use this for initialization
	void Start () 
    {

        //player = transform.parent.parent.GetComponent<Player>();

        //maxhp = player.hp;
        //hp = maxhp;
        //if(maxhp<=0)
        //{
         //   Debug.Log("不正な体力値です");
        //}

        
	}
	
	// Update is called once per frame
	void Update () 
    {
        //hp = player.hp;

        //transform.localScale = new Vector3(hp / maxhp,1,1);
	}
}
