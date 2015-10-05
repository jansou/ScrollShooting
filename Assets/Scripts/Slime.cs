using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {
	Spaceship spaceship;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();

		GameObject o = (GameObject)Instantiate(Resources.Load("ShotPosition"));
		o.transform.parent = transform;
		o.transform.localPosition = new Vector3(0,0,0);

		while (true) 
		{
			//子要素を全て取得する
			for(int i=0;i<transform.childCount;++i)
			{
				Transform shotPosition = transform.GetChild(i);

				shotPosition.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60)); 
				
				//ShotPositionの位置/角度で弾を撃つ
				spaceship.Shot(shotPosition);
			}
			
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
