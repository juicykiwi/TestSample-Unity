using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BeginDragHandleHelper : MonoBehaviour, IBeginDragHandler {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log("Call OnBeginDrag()");
	}
}
