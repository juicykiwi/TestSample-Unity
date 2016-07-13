using UnityEngine;
using System.Collections;

public class UICameraManager : MonoBehaviour {

	static UICameraManager _instance = null;
	public static UICameraManager GetInstance() { return _instance; }

	public float _cameraMoveSpeed = 1.0f;
	public Vector2 _destPos = Vector2.zero;

	void Awake () {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		MoveCamera();
	}

	public void MoveCamera()
	{
		Vector2 cameraPos = new Vector2(this.transform.position.x, this.transform.position.y);

		if (Vector2.Distance(cameraPos, _destPos) <= 0.1f)
		{
			_destPos = cameraPos;
			return;
		}

		Vector2 direction = (_destPos - cameraPos).normalized;

		Vector2 movePos = direction * _cameraMoveSpeed * Time.deltaTime;
		this.transform.Translate(movePos.x, movePos.y, 0.0f);
	}	
}
