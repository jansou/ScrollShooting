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
        if (IsPlaying() == false && Event.current.type == EventType.MouseDown && state != 0)
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        audioSource.PlayOneShot(tapSE);
        StartCoroutine("FadeStart");
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
	}

    /*
	// Update is called once per frame
	void Update () 
	{
        for (int i = 0; i < Input.touchCount; ++i)
        {
            //タッチ情報を取得する
            Touch touch = Input.GetTouch(i);

            //ゲーム中ではなく、タッチ直後であればtrueを返す。
            if (IsPlaying() == false && touch.phase==TouchPhase.Began)
            {
                GameStart();
            }
        }

        //ゲーム中ではなく、マウスクリックされたらtrueを返す。
        if (IsPlaying() == false && Input.GetMouseButtonDown(0))
        {
            GameStart();
        }

        /*
            //ゲーム中ではなく、Xキーを押されたらtrueを返す。
            if (IsPlaying() == false && Input.GetKeyDown(KeyCode.X))
            {
                GameStart();
            }
         
	}
    */


	public void GameOver()
	{
        // ハイスコアの保存
        FindObjectOfType<Score>().Save();

		//ゲームオーバー時に、タイトルを表示する
		title.SetActive (true);
	}

	//void GameStart()
	//{
      //  Application.LoadLevel("Home");
        /*
        //delete enemy bullet
        GameObject[] enemyBullets;

        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet);
        }
        //delete player bullet
        GameObject[] playerBullets;

        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        foreach (GameObject playerBullet in playerBullets)
        {
            Destroy(playerBullet);
        }

        //ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        title.SetActive(false);

		Instantiate (party, party.transform.position, party.transform.rotation);
         */
	//}

	public bool IsPlaying()
	{
		//ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}
}
