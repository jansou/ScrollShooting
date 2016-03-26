using UnityEngine;
using System.Collections;

public class RecoveryField : MonoBehaviour {
	public Party party;

	public int frame = 0;
	public int recoveryInterval = 60;
	// Use this for initialization
	void Start () {
		party = transform.parent.parent.GetComponent<Party>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0){
			return;
		}
		if(frame == recoveryInterval){
			for(int i=0; i<party.transform.childCount; ++i){
				party.transform.GetChild(i).GetComponent<Player>().recoveryHP(1);
			}
			frame = 0;
		}
		else{
			++frame;
		}
	}
}
