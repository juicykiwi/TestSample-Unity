using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	protected static InputManager _instance = null;
	public static InputManager GetInstance() { return _instance; }

//	TouchPhase _mousePhase = TouchPhase.Canceled;
	
	void Awake () {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate () {

	}

	public TouchPhase GetTouchState()
	{
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) == true)
		{
			return TouchPhase.Began;
		}
		else if (Input.GetMouseButtonUp(0) == true)
		{
			return TouchPhase.Ended;
		}

		return TouchPhase.Canceled;
#else
		if (Input.touchCount <= 0)
			return TouchPhase.Canceled;

		return Input.GetTouch(0).phase;			
#endif	        
	}

	public Vector3 GetTouchPosition()
	{
#if UNITY_EDITOR
		return Input.mousePosition;
#else
		if (Input.touchCount <= 0)
			return Vector3.zero;
		
		return Input.GetTouch(0).position;
#endif
	}

//	public void UpdateTouchStateFromMouse()
//	{
//		switch (_mousePhase)
//		{
//		case TouchPhase.Began:
//		{
//			if (Input.GetMouseButtonUp(0) == true)
//			{
//				_mousePhase = TouchPhase.Ended;
//			}
//		}
//			break;
//
//		case TouchPhase.Moved:
//		{
//			if (Input.GetMouseButtonUp(0) == true)
//			{
//				_mousePhase = TouchPhase.Ended;
//			}
//		}
//			break;
//
//		case TouchPhase.Stationary:
//		{
//			if (Input.GetMouseButtonUp(0) == true)
//			{
//				_mousePhase = TouchPhase.Ended;
//			}
//		}
//			break;
//
//		case TouchPhase.Ended:
//		{
//			if (Input.GetMouseButtonDown(0) == true)
//			{
//				_mousePhase = TouchPhase.Began;
//			}
//		}
//			break;
//
//		case TouchPhase.Canceled:
//		{
//			_mousePhase = TouchPhase.Ended;
//		}
//			break;
//		}
//	}
}
