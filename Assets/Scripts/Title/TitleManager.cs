using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour 
{
	//playerプレハブ
	public GameObject party;

	//タイトル
	private GameObject title;
    
    //SE関係
    AudioSource audioSource;
    public AudioClip tapSE;

    //Fade関係
    public GameObject fadeObject;
    Image fadeImage;
    int alpha = 255;
    private int state=0;

	public Canvas dialogCanvas;


	// Use this for initialization
	void Start () 
	{
        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        //


        //Fade関係
        fadeImage = fadeObject.GetComponent<Image>();
        fadeObject.SetActive(false);

		//Titleゲームオブジェクトを検索し取得する
		title=GameObject.Find("Title");

		dialogCanvas.enabled = false;

        StartCoroutine("FadeIn");
	}

    IEnumerator FadeIn()
    {
        while (true)
        {
            if (state == 0)
            {
                fadeObject.SetActive(true);

                if (alpha < 5)
                {
                    state += 1;
                    yield return null;
                }

                fadeImage.color = new Color(1, 1, 1, alpha / 255.0f);
                alpha -= 5;
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }
	
    void OnGUI()
    {
        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (dialogCanvas.enabled == false && Event.current.type == EventType.MouseDown && state != 0)
        {
			++state;
            GameStart();
        }
    }

    public void GameStart()
    {
        audioSource.PlayOneShot(tapSE);
        StartCoroutine("FadeStart");
    }

	public void GameRestart(){
		dialogCanvas.enabled = false;
	}
	public void GameExit(){
		Application.Quit();
	}

    IEnumerator FadeStart()
    {
        fadeObject.SetActive(true);
        while (true)
        {
            fadeImage.color = new Color(1, 1, 1, alpha / 255.0f);
            alpha += 5;
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
	{
		if(alpha > 250)
        {
            Application.LoadLevel("Home");
		}

		if(Input.GetKeyDown(KeyCode.Escape) /*&& Application.platform == RuntimePlatform.Android*/ && state == 1){
			dialogCanvas.enabled = true;
		}
	}


	public void GameOver()
	{
        // ハイスコアの保存
        FindObjectOfType<Score>().Save();

		//ゲームオーバー時に、タイトルを表示する
		title.SetActive (true);
	}
	

	public bool IsPlaying()
	{
		//ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}
}
