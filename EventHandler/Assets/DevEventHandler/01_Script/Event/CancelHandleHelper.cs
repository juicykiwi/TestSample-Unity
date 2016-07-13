using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CancelHandleHelper : MonoBehaviour, ICancelHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCancel (BaseEventData eventData)
	{
		Debug.Log("Call OnCancel()");
	}
}
