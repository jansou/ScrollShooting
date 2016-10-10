using UnityEngine;
using System.Collections;

public class BGManager : MonoBehaviour {

    public Material normalBG;
    public Material normalBG_bar;
    public Material specialBG;
    public Material specialBG_bar;

    public GameObject BG;
    public GameObject BG_bar;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
        //SE関係
        //audioSource = gameObject.GetComponent<AudioSource>();
        //
	}

    public void ChangeSpacialBG()
    {
        BG.GetComponent<Renderer>().material = specialBG;
        //BG.GetComponent<Renderer>().material.Lerp
        //    (BG.GetComponent<Renderer>().material, specialBG, 2.0f);
        BG_bar.GetComponent<Renderer>().material = specialBG_bar;
    }

    public void ChangeNormalBG()
    {
        BG.GetComponent<Renderer>().material = normalBG;
        BG_bar.GetComponent<Renderer>().material = normalBG_bar;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
