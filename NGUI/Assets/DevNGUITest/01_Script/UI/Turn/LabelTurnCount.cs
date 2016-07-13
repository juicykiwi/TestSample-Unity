using UnityEngine;
using System.Collections;

public class LabelTurnCount : MonoBehaviour {

	UILabel _uiLabel = null;

	void Awake () {
		_uiLabel = this.GetComponent<UILabel>();
		TurnManager.instance._labelTurnCount = _uiLabel;
	}

	// Use this for initialization
	void Start () {
		_uiLabel.text = TurnManager.instance._curTurn.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
