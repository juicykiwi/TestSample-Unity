using UnityEngine;
using System.Collections;

public class MainCameraHelper : MonoBehaviour {

	public Transform _target = null;

	Vector3 _beginPos = Vector3.zero;

	void Awake () {
		_beginPos = transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_target)
		{
			Vector3 pos = Vector3.zero;
			pos.x = _target.position.x + _beginPos.x - 3.0f;
			pos.z = _target.position.z + _beginPos.z - 5.0f;
			pos.y = _beginPos.y;

			transform.position = pos;
		}
	}
}
