using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerUpHandleHelper : MonoBehaviour, IPointerUpHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Debug.Log("Call OnPointerUp()");
	}
}
