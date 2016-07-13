using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EndDragHandleHelper : MonoBehaviour, IEndDragHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		Debug.Log("Call OnBeginDrag()");
	}
}
