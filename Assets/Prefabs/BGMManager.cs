using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {
	public AudioClip stageBGM;
	public AudioClip bossBGM;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        //SE関係
        //audioSource = gameObject.GetComponent<AudioSource>();
        //
	}

    public void PauseBGM()
    {
        //GetComponent<AudioSource>().clip = bossBGM;
        audioSource.Pause();
    }

    public void PlayBGM()
    {
        //GetComponent<AudioSource>().clip = bossBGM;
        audioSource.Play();
    }

    public void StopBGM()
    {
        //GetComponent<AudioSource>().clip = bossBGM;
        audioSource.Stop();
    }

	public void SetBossBGM(){
		//GetComponent<AudioSource>().clip = bossBGM;
        audioSource.clip = bossBGM;
        audioSource.Play();
	}
	public void SetStageBGM(){
		//GetComponent<AudioSource>().clip = stageBGM;
        audioSource.clip = stageBGM;
        audioSource.Play();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
