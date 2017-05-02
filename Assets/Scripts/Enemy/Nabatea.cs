using UnityEngine;
using System.Collections;

public class Nabatea : MonoBehaviour {	
	Spaceship spaceship;
	EnemyCommon common;
	Enemy enemy;
	
	Transform st;
	Transform pt;

	public GameObject stoneAlex;
	public GameObject stoneGuylus;
	public GameObject stoneNely;
	public GameObject stoneRinmaru;
	public GameObject stoneMedhu;

	GameObject[] stones;

    public AudioClip skillSE;
    AudioSource audioSource;

    public int shotPower = 5;

    public int stateCount = 0;

	// Use this for initialization
	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		common = GetComponent<EnemyCommon>();
		enemy = GetComponent<Enemy>();
		common.Init();

        //
        audioSource = gameObject.GetComponent<AudioSource>();
        //

		st = common.CreateShotPosition();
		pt = FindObjectOfType<Party>().transform;

		yield return new WaitForEndOfFrame();
		
		yield return StartCoroutine("Stop");

        audioSource.PlayOneShot(skillSE);
        FindObjectOfType<MessageWindow>().showMessage("「星霜の記憶」");
		stones = new GameObject[5];
		stones[0] = (GameObject)Instantiate(stoneAlex,transform.position,Quaternion.identity);
		stones[1] = (GameObject)Instantiate(stoneGuylus,transform.position,Quaternion.identity);
		stones[2] = (GameObject)Instantiate(stoneNely,transform.position,Quaternion.identity);
		stones[3] = (GameObject)Instantiate(stoneRinmaru,transform.position,Quaternion.identity);
		stones[4] = (GameObject)Instantiate(stoneMedhu,transform.position,Quaternion.identity);

		StartCoroutine("RotateStones");
		
		StartCoroutine("Attack");
        StartCoroutine("StonesSummon");

	}

	IEnumerator Stop(){
		FindObjectOfType<MessageWindow>().showMessage("魔女ナバテア");
		yield return new WaitForSeconds(4);
		enemy.MoveStop();
	}
	IEnumerator RotateStones(){
		while(true){
			for(int i=0; i<5; ++i){
				if(stones[i]){
					float d = Mathf.PI*2/5*i + dig;
					stones[i].transform.position = transform.position + n * new Vector3(Mathf.Cos (d),Mathf.Sin(d),0);
				}
			}
			n = Mathf.Min(1.2f,n+Time.deltaTime);
			dig += Time.deltaTime * 2;
			yield return new WaitForEndOfFrame();
		}
	}

    IEnumerator StonesSummon()
    {
        while (true)
        {
            if (enemy.hp < enemy.maxHP / 1.5 && stateCount == 0)
            {
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("「戦刃の記憶」");
                stones = new GameObject[5];
                stones[0] = (GameObject)Instantiate(stoneAlex, transform.position, Quaternion.identity);
                stones[1] = (GameObject)Instantiate(stoneGuylus, transform.position, Quaternion.identity);
                stones[2] = (GameObject)Instantiate(stoneAlex, transform.position, Quaternion.identity);
                stones[3] = (GameObject)Instantiate(stoneRinmaru, transform.position, Quaternion.identity);
                stones[4] = (GameObject)Instantiate(stoneGuylus, transform.position, Quaternion.identity);

                stateCount += 1;
            }
            else if (enemy.hp < enemy.maxHP / 2 && stateCount == 1)
            {
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("「幻魔の記憶」");
                stones = new GameObject[5];
                stones[0] = (GameObject)Instantiate(stoneNely, transform.position, Quaternion.identity);
                stones[1] = (GameObject)Instantiate(stoneNely, transform.position, Quaternion.identity);
                stones[2] = (GameObject)Instantiate(stoneNely, transform.position, Quaternion.identity);
                stones[3] = (GameObject)Instantiate(stoneMedhu, transform.position, Quaternion.identity);
                stones[4] = (GameObject)Instantiate(stoneMedhu, transform.position, Quaternion.identity);

                stateCount += 1;
            }
            else if (enemy.hp < enemy.maxHP / 3 && stateCount == 2)
            {
                audioSource.PlayOneShot(skillSE);
                FindObjectOfType<MessageWindow>().showMessage("「宙のミアズマ」");
                stones = new GameObject[5];
                stones[0] = (GameObject)Instantiate(stoneRinmaru, transform.position, Quaternion.identity);
                stones[1] = (GameObject)Instantiate(stoneNely, transform.position, Quaternion.identity);
                stones[2] = (GameObject)Instantiate(stoneNely, transform.position, Quaternion.identity);
                stones[3] = (GameObject)Instantiate(stoneRinmaru, transform.position, Quaternion.identity);
                stones[4] = (GameObject)Instantiate(stoneRinmaru, transform.position, Quaternion.identity);

                stateCount += 1;
            }

            yield return new WaitForSeconds(0.0f);


        }
    }

	IEnumerator Attack(){
		while(true){
			yield return StartCoroutine("SaintPillar");
			yield return new WaitForSeconds(10.0f);

		}

	}

	IEnumerator SaintPillar(){
		spaceship.GetAnimator().SetTrigger("Skill");
		//audioSource.PlayOneShot(skillSE);
		//FindObjectOfType<MessageWindow>().showMessage("ジャッジメント");

		float x = Random.Range(4.4f,5.0f);
		for(int i=0; i<9; ++i){
			//audioSource.PlayOneShot(shootSE);
			st.position = new Vector3(x-i*1.2f,6.0f,0.0f);
			common.Shot(st,180,shotPower,20,BulletManager.BulletType.RayBullet);
			yield return new WaitForSeconds(0.4f);
		}
	}
	float n = 0.0f;
	float dig = 0.0f;
	
	// Update is called once per frame
	void Update () {

	}
}
