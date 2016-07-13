using UnityEngine;
using System.Collections;

public class TextState : MonoBehaviour {
	
	public UILabel _labelState = null;

	void Awake () {
		_labelState = this.GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
