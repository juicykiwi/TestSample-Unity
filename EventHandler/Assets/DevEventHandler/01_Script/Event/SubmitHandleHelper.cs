using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SubmitHandleHelper : MonoBehaviour, ISubmitHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSubmit (BaseEventData eventData)
	{
		Debug.Log("Call OnSubmit()");
	}
}
