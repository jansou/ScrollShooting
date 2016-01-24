using UnityEngine;
using System.Collections;

public class TalkEvent : MonoBehaviour {
	Spaceship spaceship;
	EnemyCommon common;
    Enemy enemy;

    //SE関係
    //public AudioClip shootSE;
    public AudioClip skillSE;
    AudioSource audioSource;

    //テキスト関係/////////////////////////
    //テキストを格納
    public string[] texts;

    //現在のText
    private int currentText;
    ////////////////////////////

	
    // Use this for initialization
	IEnumerator Start () 
    {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
        enemy = GetComponent<Enemy>();
		common.Init();

        //SE関係
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = skillSE;
        //

        currentText = 0;

		yield return new WaitForEndOfFrame();

        StartCoroutine("Stop");

        //yield return new WaitForSeconds(2);
        StartCoroutine("Talk"); 

		yield break;
	}

    IEnumerator Talk()
    {
        while (true)
        {
            //Textが存在しなければこれを消去する
            if (texts.Length == 0 || texts.Length <= currentText)
            {
                Destroy(gameObject);
                yield break;
            }

            audioSource.PlayOneShot(skillSE);
            //文章を適用する
            FindObjectOfType<MessageWindow>().showMessage(texts[currentText]);

            ++currentText;

            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2);
        enemy.MoveStop();
    }

	void Update () {
		
	}
}
