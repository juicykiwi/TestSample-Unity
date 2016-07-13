using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UpdateSelectedHandleHelper : MonoBehaviour, IUpdateSelectedHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnUpdateSelected (BaseEventData eventData)
	{
		Debug.Log("Call OnUpdateSelected()");
	}
}
