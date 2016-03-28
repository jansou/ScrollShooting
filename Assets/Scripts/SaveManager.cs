using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {
	const string stagePref = "ArrivedStage";
	const string moneyPref ="NowMoney";
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

	public struct ItemNumInfo{
		public int normalHerb;
		public int niceHerb;
		public int greatHerb;
		public int lifeOrb;
		public int greatLifeOrb;
	}
	const string normalHerbPref = "NormalHerb";
	const string niceHerbPref = "NiceHerb";
	const string greatHerbPref = "GreatHerb";
	const string lifeOrbPref = "LifeOrb";
	const string greatLifeOrbPref = "GreatLifeOrb";

	public Status alex;
	public Status guylus;
	public Status nely;
	public Status rinmaru;
	public Status medhu;

	public ItemNumInfo itemNumInfo;

	public int money;

	bool loaded = false;
	public bool IsLoaded(){
		return loaded;
	}

    public bool isDebug=false;

    int getDefaultLevel(string prefName)//加入時のレベル
    {
        if (prefName == alexPref)
        {
            return 1;
        }
        if (prefName == guylusPref)
        {
            return 1;
        }
        if (prefName == nelyPref)
        {
            return 6;
        }
        if (prefName == rinmaruPref)
        {
            return 1;
        }
        if (prefName == medhuPref)
        {
            return 1;
        }
        return 1;
    }
    int getDefaultEXP(string prefName)
    {
        return (200 * (getDefaultLevel(prefName) * 2 - 1));
    }

	Status GetStatus(string prefName){
		Status st;
		st.level = PlayerPrefs.GetInt(GetLevelPref(prefName),getDefaultLevel(prefName));
		st.exp = PlayerPrefs.GetInt(GetExpPref(prefName),getDefaultEXP(prefName));
		st.isJoining = false;
		return st;
	}
	void SetStatus(Status st,string prefName){
		PlayerPrefs.SetInt(GetLevelPref(prefName),st.level);
		PlayerPrefs.SetInt(GetExpPref(prefName),st.exp);
	}

	ItemNumInfo GetItemNumInfo(){
		ItemNumInfo info;
		info.normalHerb = PlayerPrefs.GetInt(normalHerbPref+"Num",0);
		info.niceHerb = PlayerPrefs.GetInt(niceHerbPref+"Num",0);
		info.greatHerb = PlayerPrefs.GetInt(greatHerbPref+"Num",0);
		info.lifeOrb= PlayerPrefs.GetInt(lifeOrbPref+"Num",0);
		info.greatLifeOrb = PlayerPrefs.GetInt(greatLifeOrbPref+"Num",0);
		return info;
	}
	void SetItemNumInfo(){
		SetItemNum(normalHerbPref+"Num",itemNumInfo.normalHerb);
		SetItemNum(niceHerbPref+"Num",itemNumInfo.niceHerb);
		SetItemNum(greatHerbPref+"Num",itemNumInfo.greatHerb);
		SetItemNum(lifeOrbPref+"Num",itemNumInfo.lifeOrb);
		SetItemNum(greatLifeOrbPref+"Num",itemNumInfo.greatLifeOrb);
	}
	void SetItemNum(string prefName,int itemNum){
		if(itemNum > 0){
			PlayerPrefs.SetInt(prefName,itemNum);
		}
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
        if(!isDebug) arrivedStage = PlayerPrefs.GetInt(stagePref,1);

		money = PlayerPrefs.GetInt(moneyPref,0);

		itemNumInfo = GetItemNumInfo();

		alex = GetStatus(alexPref);
		guylus = GetStatus(guylusPref);
		nely = GetStatus(nelyPref);
		rinmaru = GetStatus(rinmaruPref);
		medhu = GetStatus(medhuPref);
		
		alex.isJoining = true;
		guylus.isJoining = true;
		nely.isJoining = arrivedStage >= 4;
		rinmaru.isJoining = arrivedStage >= 6;
		medhu.isJoining = arrivedStage >= 6;

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
		PlayerPrefs.SetInt(moneyPref,money);

		SetItemNumInfo();

		SetStatus(alex,alexPref);
		SetStatus(guylus,guylusPref);
		SetStatus(nely,nelyPref);
		SetStatus(rinmaru,rinmaruPref);
		SetStatus(medhu,medhuPref);

		Debug.Log ("Saved");
	}
}
