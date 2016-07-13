using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerEnterHandleHelper : MonoBehaviour, IPointerEnterHandler {

	public bool _isChangeColor = false;
	public UnityEvent _event = new UnityEvent();

	// Method

		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Debug.Log("Call OnPointerEnter()");

		if (_isChangeColor == true)
		{
			if (_event == null)
				return;

			_event.Invoke();
		}
	}
}
