using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerDownHandleHelper : MonoBehaviour, IPointerDownHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log("Call OnPointerDown()");
	}
}
