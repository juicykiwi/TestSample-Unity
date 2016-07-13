using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerExitHandleHelper : MonoBehaviour, IPointerExitHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		Debug.Log("Call OnPointerExit()");
	}
}
