﻿using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{
	//スクロールするスピード
	public float speed = 0.1f;

	// Use this for initialization
	void Start () 
    {
	    //画面右上のワールド座標をビューレポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //スケールを求める。
        Vector2 scale = max * 2;

        //スケールを変更。
        transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float x = Mathf.Repeat(Time.time * speed, 1);

		//Yの値がずれていくオフセットを作成
		Vector2 offset = new Vector2 (x, 0);

		//マテリアルにオフセットを設定する
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
