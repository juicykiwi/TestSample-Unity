using UnityEngine;
using System.Collections;

public class ButtonActor : MonoBehaviour {

	Actor _actor = null;

	void Awake () {
		_actor = this.GetComponentInParent<Actor>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress(bool isDown)
	{
		Debug.Log("ButtonActor OnPress()");

		SetDestPosInWorld(isDown);
	}

	void OnClick()
	{
//		Debug.Log("Actor OnClick()");
	}

	void SetDestPosInWorld(bool isDown)
	{
//		TouchPhase touchPhase = InputManager.GetInstance().GetTouchState();
		Vector3 touchPos = InputManager.GetInstance().GetTouchPosition();

//		if (touchPhase == TouchPhase.Began)
		if (isDown == true)
		{
		}
//		else if (touchPhase == TouchPhase.Ended)
		else if (isDown == false)
		{
			_actor.destPosInWorld = UICamera.mainCamera.ScreenToWorldPoint(touchPos);
		}
	}

	void SetDestPosInScreen(bool isDown)
	{
//		Debug.Log("Actor OnPress()");
		
		TouchPhase touchPhase = InputManager.GetInstance().GetTouchState();
		Vector3 touchPos = InputManager.GetInstance().GetTouchPosition();
		
		if (touchPhase == TouchPhase.Began)
		{
//			Debug.Log("Actor OnPress() TouchPhase.Began");
//			Debug.Log(string.Format("{0}, {1}, {2}", touchPos.x, touchPos.y, touchPos.z));
		}
		else if (touchPhase == TouchPhase.Ended)
		{
//			Debug.Log("Actor OnPress() TouchPhase.Ended");
//			Debug.Log(string.Format("{0}, {1}, {2}", touchPos.x, touchPos.y, touchPos.z));
			
			_actor.destPosInScreen = touchPos;
		}
	}
}
