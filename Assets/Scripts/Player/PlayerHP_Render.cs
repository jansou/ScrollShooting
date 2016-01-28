using UnityEngine;
using System.Collections;

public class PlayerHP_Render : MonoBehaviour 
{
    //Player player;

    float maxhp=0;
    float hp = 0;

	float scalex=0; // default scaleX of gauge
	// Use this for initialization

	void Awake(){
		scalex = transform.localScale.x;
	}
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

	public void InitHP(int maxHP){

		maxhp = maxHP;
		hp = maxHP;
		SetHP (maxHP);
	}

	public void SetHP(int nowHP){
		hp = nowHP;
		Vector3 s = transform.localScale;
		s.x = scalex * hp / maxhp;
		//transform.localScale = new Vector3(hp / maxhp,1,1);
		transform.localScale = s;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //hp = player.hp;

        //transform.localScale = new Vector3(hp / maxhp,1,1);
	}
}
