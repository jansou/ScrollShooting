using UnityEngine;
using System.Collections;

public class WaveInfo : MonoBehaviour 
{
    public GameObject[] enemies;
    public Vector3[] enemypositions;
    private bool isDestroyed = false;
    public bool isBoss = false;
    public bool useSpecialBGM=false;

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
