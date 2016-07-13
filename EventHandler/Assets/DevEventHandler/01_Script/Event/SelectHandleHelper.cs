using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectHandleHelper : MonoBehaviour, ISelectHandler {

	// Use this for initialization
	void Start () {
		Button button;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSelect (BaseEventData eventData)
	{
		Debug.Log("Call OnSelect()");
	}
}
