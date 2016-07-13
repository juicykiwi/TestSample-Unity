using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InitializePotentialDragHandleHelper : MonoBehaviour, IInitializePotentialDragHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnInitializePotentialDrag (PointerEventData eventData)
	{
		Debug.Log("Call OnInitializePotentialDrag()");
	}
}
