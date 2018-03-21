using UnityEngine;
using System.Collections;

public class BGManager : MonoBehaviour {

    //開始時の背景
    public Material normalBG;
    public Material normalBG_bar;
    //演出用の特別な背景
    public Material specialBG;
    public Material specialBG_bar;
    //エンディング用背景その1（普段は使わない）
    public Material EDBG1;
    public Material EDBG1_bar;
    //エンディング用背景その2（普段は使わない）
    public Material EDBG2;
    public Material EDBG2_bar;
    //エンディング用背景その3（普段は使わない）
    public Material EDBG3;
    public Material EDBG3_bar;
    //エンディング用背景その4（普段は使わない）
    public Material EDBG4;
    public Material EDBG4_bar;


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

    public void ChangeNormalBG()//開始時の背景に戻す
    {
        BG.GetComponent<Renderer>().material = normalBG;
        BG_bar.GetComponent<Renderer>().material = normalBG_bar;
    }


    public void ChangeEDBG(int num=1)//ED用の背景。1~4しか存在しない
    {
        switch (num)
        {
            case 1:
                {
                    BG.GetComponent<Renderer>().material = EDBG1;
                    BG_bar.GetComponent<Renderer>().material = EDBG1_bar;
                    break;
                }
            case 2:
                {
                    BG.GetComponent<Renderer>().material = EDBG2;
                    BG_bar.GetComponent<Renderer>().material = EDBG2_bar;
                    break;
                }
            case 3:
                {
                    BG.GetComponent<Renderer>().material = EDBG3;
                    BG_bar.GetComponent<Renderer>().material = EDBG3_bar;
                    break;
                }
            case 4:
                {
                    BG.GetComponent<Renderer>().material = EDBG4;
                    BG_bar.GetComponent<Renderer>().material = EDBG4_bar;
                    break;
                }
            default:
                break;
        }
    }




	// Update is called once per frame
	void Update () {
	
	}
}
