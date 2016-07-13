using UnityEngine;
using System.Collections;

public class BattleGround : MonoBehaviour {

	static BattleGround _instance = null;
	public static BattleGround GetInstance() { return _instance; }

	protected UIPanel _uiPanel = null;

	public Rect _rect;
	public Rect _rectInWorld;

	void Awake () {
		_instance = this;

		_uiPanel = GetComponent<UIPanel>();
	}

	// Use this for initialization
	void Start () {

		Vector2 viewSize = _uiPanel.GetViewSize();

		Vector2 screenPos = UICamera.mainCamera.WorldToScreenPoint(this.transform.position);
		Vector2 screenSize = UIRootManager.GetInstance().PixelToScreenPoint(viewSize);
//		Debug.Log(string.Format("this.transform.position : {0}, {1}, {2}", this.transform.position.x, this.transform.position.y, this.transform.position.z));

		_rect.x = ((UIRootManager.GetInstance()._screenWidth - screenPos.x) / 2.0f) + screenPos.x;
		_rect.y = ((UIRootManager.GetInstance()._screenHeight - screenSize.y) / 2.0f) + screenPos.y;
		_rect.width = screenSize.x;
		_rect.height = screenSize.y;

//		Debug.Log(string.Format("_rect : {0}, {1}, {2}, {3}", _rect.x, _rect.y, _rect.width, _rect.height));

		Vector2 worldSize = UIRootManager.GetInstance().ScreenToWorldSize(screenSize);
		_rectInWorld.x = this.transform.position.x - (worldSize.x / 2.0f);
		_rectInWorld.y = this.transform.position.y - (worldSize.y / 2.0f);
		_rectInWorld.width = worldSize.x;
		_rectInWorld.height = worldSize.y;

//		Debug.Log(string.Format("_rectInWorld : {0}, {1}, {2}, {3}", _rectInWorld.x, _rectInWorld.y, _rectInWorld.width, _rectInWorld.height));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isDown)
	{
		Debug.Log("BattleGround OnPress()");

//		TouchPhase touchPhase = InputManager.GetInstance().GetTouchState();
		Vector3 touchPos = InputManager.GetInstance().GetTouchPosition();

//		if (touchPhase == TouchPhase.Began)
		if (isDown == true)
		{
		}
//		else if (touchPhase == TouchPhase.Ended)
		else if (isDown == false)
		{
			Vector3 pos = UICamera.mainCamera.ScreenToWorldPoint(touchPos);
//			pos.z = UICamera.mainCamera.transform.position.z;

//			UICamera.mainCamera.transform.position = pos;

			UICameraManager.GetInstance()._destPos = pos;
		}
	}

	void OnDrag (Vector2 delta) {
		Debug.Log("BattleGround OnDrag()");

//		UICameraManager.GetInstance().MoveCamera(delta);
	}
}
