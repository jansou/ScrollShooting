using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
	public GameObject mobileStick;
	UnityStandardAssets.CrossPlatformInput.Joystick joystick;
	// Use this for initialization
	void Start () {
		joystick = mobileStick.transform.FindChild("MobileJoystick").GetComponent<UnityStandardAssets.CrossPlatformInput.Joystick>();
	
	}

	Vector3 center;
	bool moving = false;
	// Update is called once per frame
	void Update () {
		if(moving){
			joystick.DrawByPos(Input.mousePosition);
		}

		if(Input.GetMouseButtonDown(0)){
			center = Input.mousePosition;
			//mobileStick.transform.position = center;
			joystick.SetStartPos(center);
			moving = true;
		}
		if(Input.GetMouseButtonUp(0)){
			moving = false;
			joystick.SetStartPos(center);
		}
	}
}
