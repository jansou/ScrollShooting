using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {
	public AudioClip stageBGM;
	public AudioClip bossBGM;
	// Use this for initialization
	void Start () {
	}

	public void SetBossBGM(){
		GetComponent<AudioSource>().clip = bossBGM;
	}
	public void SetStageBGM(){
		GetComponent<AudioSource>().clip = stageBGM;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
