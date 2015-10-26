using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {

	Spaceship spaceship;
	Transform shotPositions;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();

		GameObject oo = new GameObject();
		oo.name = "shotPositions";
		oo.transform.parent = transform;
		oo.transform.localPosition = new Vector3(0,0,0);
		shotPositions = oo.transform;

		for(int i=0; i<2; ++i){
			GameObject o = (GameObject)Instantiate(Resources.Load("ShotPosition"));
			o.transform.parent = shotPositions;
			o.transform.localPosition = new Vector3(0,0,0);
		}
		
		while (true) 
		{
			//子要素を全て取得する
			for(int i=0;i<shotPositions.childCount;++i)
			{
				Transform shotPosition = shotPositions.GetChild(i);
				
				shotPosition.localRotation = Quaternion.Euler(0,0,60+Random.Range(0,60)); 
				
				//ShotPositionの位置/角度で弾を撃つ
				spaceship.Shot(shotPosition,1);

			}
						
			//shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
