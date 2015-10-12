using UnityEngine;
using System.Collections;

public class Party : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void addExp(int point)
    {
        Player[] playerList;
        playerList = GetComponentsInChildren<Player>();

        //GameObject party = GameObject.Find("party");

        //経験値を全プレイヤーに追加
        foreach (Player player in playerList)
        {
            player.addExp(point);
        }
    }
}
