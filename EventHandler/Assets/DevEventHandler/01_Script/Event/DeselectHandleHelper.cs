using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DeselectHandleHelper : MonoBehaviour, IDeselectHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDeselect (BaseEventData eventData)
	{
		Debug.Log ("Call OnDeselect()");
	}
}
