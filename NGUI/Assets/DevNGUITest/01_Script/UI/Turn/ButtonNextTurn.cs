using UnityEngine;
using System.Collections;

public class ButtonNextTurn : MonoBehaviour {

	void Awake () {

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		TurnManager.instance.TurnIncrease();
	}
}
