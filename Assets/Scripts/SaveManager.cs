using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {
	const string stagePref = "ArrivedStage";
	const string alexPref = "Alex";
	const string guylusPref = "Guylus";
	const string nelyPref = "Nely";
	const string rinmaruPref = "Rinmaru";
	const string medhuPref = "Medhu";

	public int arrivedStage;

	public struct Status{
		public int level;
		public int exp;
		public bool isJoining;
	};
	public Status alex;
	public Status guylus;
	public Status nely;
	public Status rinmaru;
	public Status medhu;

	bool loaded = false;
	public bool IsLoaded(){
		return loaded;
	}


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

	void Awake(){
		DontDestroyOnLoad(this);

		if(FindObjectsOfType<SaveManager>().Length > 1){
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {

	}
	public void Load(){
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

		Debug.Log ("Loaded");
		loaded = true;
	}
	
	// Update is called once per frame
	void Update () {
		debugKey ();
	}

	void debugKey(){
		if(Input.GetKeyDown(KeyCode.D)){
			PlayerPrefs.DeleteAll();
			Debug.Log("All Saves Deleted.");
			Start ();
		}
	}

	public void SaveData(){
		PlayerPrefs.SetInt(stagePref,arrivedStage);

		SetStatus(alex,alexPref);
		SetStatus(guylus,guylusPref);
		SetStatus(nely,nelyPref);
		SetStatus(rinmaru,rinmaruPref);
		SetStatus(medhu,medhuPref);

		Debug.Log ("Saved");
	}
}
