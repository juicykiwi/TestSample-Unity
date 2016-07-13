using UnityEngine;
using System;
using System.Collections;

public class TouchManager : MonoBehaviour {

	static TouchManager _instance = null;
	public static TouchManager instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<TouchManager>();

			return _instance;
		}
	}

	public Action<Vector3> _actionTouch = null;

	// Method

	void Awake () {
		if (_instance == null)
			_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) == true)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);
			if (hits.Length <= 0)
				return;

			foreach (RaycastHit hit in hits)
			{
				if (hit.collider == null)
					continue;

				if (hit.collider.CompareTag("Field") == false)
					continue;

				if (_actionTouch != null)
					_actionTouch(hit.point);

				break;
			}
		}
	}
}
