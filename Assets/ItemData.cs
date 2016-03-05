using UnityEngine;
using System.Collections;

public enum ItemType{
	NormalHerb,
	NiceHerb,
	GreatHerb,
	LifeOrb,
	GreatLifeOrb,
	None
};

public class ItemData : MonoBehaviour {
	public string itemname;
	public string explanation;
	public int price;
	public Sprite image;
	public ItemType type;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
