using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {
	const string stagePref = "ArrivedStage";
	const string alexPref = "Alex";
	const string guylusPref = "Guylus";
	const string nelyPref = "Nely";
	const string rinmaruPref = "Rinmaru";
	const string medhuPref = "Medhu";

	int arrivedStage;

	public struct Status{
		public int level;
		public int exp;
		public bool isJoining;
	};
	public Status alex;
	Status guylus;
	Status nely;
	Status rinmaru;
	Status medhu;

	Status GetStatus(string prefName){
		Status st;
		st.level = PlayerPrefs.GetInt(GetLevelPref(prefName),1);
		st.exp = PlayerPrefs.GetInt(GetExpPref(prefName),1);
		st.isJoining = false;
		return st;
	}
	void SetStatus(Status st,string prefName){
		PlayerPrefs.SetInt(GetLevelPref(prefName),st.level);
		PlayerPrefs.SetInt(GetExpPref(prefName),st.exp);
	}

	string GetLevelPref(string prefName){
		return prefName + "Level";
	}
	string GetExpPref(string prefName){
		return prefName + "Exp";
	}


	// Use this for initialization
	void Start () {
		arrivedStage = PlayerPrefs.GetInt(stagePref,1);


		alex = GetStatus(alexPref);
		guylus = GetStatus(guylusPref);
		nely = GetStatus(nelyPref);
		rinmaru = GetStatus(rinmaruPref);
		medhu = GetStatus(medhuPref);

		alex.isJoining = true;
		guylus.isJoining = true;
		nely.isJoining = arrivedStage >= 4;
		rinmaru.isJoining = arrivedStage >= 6;
		medhu.isJoining = arrivedStage >= 8;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		/*
		PlayerPrefs.SetInt(stagePref,arrivedStage);

		SetStatus(alex,alexPref);
		SetStatus(guylus,guylusPref);
		SetStatus(nely,nelyPref);
		SetStatus(rinmaru,rinmaruPref);
		SetStatus(medhu,medhuPref);
		*/
	}
}
