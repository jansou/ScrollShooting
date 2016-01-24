using UnityEngine;
using System.Collections;

public class PlayerEXP_Render : MonoBehaviour {

	float nextExp = 0;
	float nowExp = 0;
	float preExp = 0;
	
	float scalex; // default scaleX of gauge
	// Use this for initialization
	void Start () 
	{	
		scalex = transform.localScale.x;
	}
	
	public void InitEXP(int preEXP,int nowEXP,int nextEXP){
		nextExp = nextEXP;
		nowExp = nowEXP;
		preExp = preEXP;
		SetEXP(nowEXP);
	}
	
	public void SetEXP(int nowEXP){
		nowExp = nowEXP;
		Vector3 s = transform.localScale;
		float needExp = (nextExp-preExp);
		s.x = scalex * (needExp-(nextExp-nowExp)) / needExp;
		transform.localScale = s;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//hp = player.hp;
		
		//transform.localScale = new Vector3(hp / maxhp,1,1);
	}
}
