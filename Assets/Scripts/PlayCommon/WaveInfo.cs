using UnityEngine;
using System.Collections;

public class WaveInfo : MonoBehaviour 
{
    public GameObject[] enemies;
    public Vector3[] enemypositions;
    private bool isDestroyed = false;
    public bool isBoss = false;
    public bool useSpecialBGM=false;
    public bool stopBGM = false;
    public bool changeSpecialBG = false;
    public bool changeNormalBG = false;

    //EDの切り替え用操作
    public int changeEDBGnum = 0;

	// Use this for initialization
	void Start () 
    {
        for(int num=0; num < enemies.Length;++num)
        {
            GameObject enemy = (GameObject)Instantiate(enemies[num], enemypositions[num], Quaternion.identity);

            enemy.transform.parent = transform;
        }
	}

    public bool GetIsDestroyed()
    { return isDestroyed; }
	
	// Update is called once per frame
	void Update () 
    {
        isDestroyed = transform.childCount == 0;
	}
}
