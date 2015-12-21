using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour 
{
	public Text stageText;
	public Image stageBack;

	public Sprite back1;
	public Sprite back2;

	int selectnum = 0;


	TextAsset tasset;

	// Use this for initialization
	void Start () 
	{

	}
	
    void OnGUI()
    {

        if (tapHit() == "ContinueButton")//それぞれのボタンを設定する
        {
            GameStart();
        }
    }
	
	public void GameStart()
	{
		switch(selectnum){
		case 0:
        	Application.LoadLevel("Stage");
			break;
		default:
			Application.LoadLevel("Stage");
			break;
		}
	}

    string tapHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
                if (hitObject)
                {
                    return hitObject.collider.gameObject.name;
                }
            }
        }
        return "none";
    }

	public void SelectStage(int num){
		selectnum = num;
		string textPath = "none";

		//stageBack.sprite =  
		switch(num){
		case 0:
			stageBack.sprite = back1;
			textPath = "Stage1";
			break;
		default:
			stageBack.sprite = back2;
			textPath = "Stage2";
			break;
		}

		tasset = (TextAsset)Resources.Load(textPath);
		if(tasset){
			stageText.text = tasset.text;
		}
		else{
			stageText.text = "テキストが存在していません。";
		}
	}
}
