using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandleHelper : MonoBehaviour, IDragHandler {

	public bool _isDragMove = false;

	// Method

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDrag (PointerEventData eventData)
	{
		Debug.Log("Call OnDrag()");

		if (_isDragMove == true)
		{
			this.transform.position = eventData.position;
		}
	}
}
