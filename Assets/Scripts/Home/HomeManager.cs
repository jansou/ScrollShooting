using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour 
{
	public Text stageText;
	public Image stageBack;

	public Transform buttonContent;

	public Sprite back1;
	public Sprite back2;

	int selectnum = 0;


	TextAsset tasset;

	// Use this for initialization
	void Start () 
	{
		SelectStage (0);
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

	public Color normalButtonColor;
	public Color selectedButtonColor;

	string GetStagePath(int num){
		/*
		switch(num){
		case 0:
			return "Stage1";
		default:
			return "Stage2";
		}
	*/
		return "Stage" + (num+1).ToString();
	}

	public void SelectStage(int num){	
		//stageBack.sprite =  
		switch(num){
		case 0:
			stageBack.sprite = back1;
			break;
		default:
			stageBack.sprite = back2;
			break;
		}

		string textPath = GetStagePath(num);
		string prevPath = GetStagePath(selectnum);

		buttonContent.FindChild(prevPath).GetComponent<Image>().color = normalButtonColor;
		buttonContent.FindChild(textPath).GetComponent<Image>().color = selectedButtonColor;
		selectnum = num;

		tasset = (TextAsset)Resources.Load(textPath);
		if(tasset){
			stageText.text = tasset.text;
		}
		else{
			stageText.text = "テキストが存在していません。";
		}
	}
}
