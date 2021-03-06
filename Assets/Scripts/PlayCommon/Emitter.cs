﻿using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour 
{
	//Waveプレハブを格納
	public GameObject[] waves;

	//現在のWave
	public int currentWave;

	//Managerコンポーネント
	public Manager manager;

	bool reset = false;

	IEnumerator Start()
	{
		//Waveが存在しなければコルーチンを終了する
		if (waves.Length == 0) 
		{
			yield break;
		}

		//Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager> ();

		while (true) 
		{
			while(manager.IsPlaying()==false)
			{
				yield return new WaitForEndOfFrame();
			}
			reset = false;

			//Waveを作成する
			GameObject wave=(GameObject)Instantiate(
				waves[currentWave],transform.position,Quaternion.identity);

			//WaveをEmitterの子要素にする
			wave.transform.parent=transform;

            WaveInfo waveInfo = wave.GetComponent<WaveInfo>();
			if(waveInfo.isBoss)//ボスにチェックが付いていればボス用のＢＧＭを再生
            {
				FindObjectOfType<BGMManager>().SetBossBGM();
			}
            if (waveInfo.useSpecialBGM)//チェックがついていればボスＢＧＭ2のような溶け別なＢＧＭを流すのに使用
            {
                FindObjectOfType<BGMManager>().SetSpecialBGM();
            }
            if (waveInfo.stopBGM)//再生中のＢＧＭを停止
            {
                FindObjectOfType<BGMManager>().StopBGM();
            }

            if (waveInfo.changeNormalBG)//背景を開始時のものに戻す
            {
                FindObjectOfType<BGManager>().ChangeNormalBG();
            }
            if (waveInfo.changeSpecialBG)//背景を特別なものに変更
            {
                FindObjectOfType<BGManager>().ChangeSpacialBG();
            }

            if (waveInfo.changeEDBGnum!=0)
            {
                FindObjectOfType<BGManager>().ChangeEDBG(waveInfo.changeEDBGnum);
            }
            //else FindObjectOfType<BGMManager>().SetStageBGM();

			//Waveの子要素のEnemyが全て削除されるまで待機する
            while (!waveInfo.GetIsDestroyed() /*wave.transform.childCount != 0*/)
			{
				yield return new WaitForEndOfFrame();
			}

			yield return new WaitForSeconds(1);

			//Waveの削除
			Destroy(wave);

            //BGM管理
			if(reset){
				currentWave = 0;
				reset = false;
				//FindObjectOfType<BGMManager>().SetStageBGM();
			}
			else if(waves.Length <= ++currentWave)
			{
				manager.GameClear();
				yield return new WaitForSeconds(3);

				string sceneName = Application.loadedLevelName;
				if (sceneName == "Stage12"){
					Application.LoadLevel("Ending");
				}
				else{
					Application.LoadLevel("Home");
				}
			}
            ///////
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
	public void ResetWave(){
		reset = true;
	}
}
