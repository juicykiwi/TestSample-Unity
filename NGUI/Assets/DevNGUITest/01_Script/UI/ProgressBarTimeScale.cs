using UnityEngine;
using System.Collections;

public class ProgressBarTimeScale : MonoBehaviour {

	public UISlider _uiSlider = null;
	public GameObject _uiLabelObject = null;

	protected UILabel _uiLabel = null;

	void Awake () {
		_uiLabel = _uiLabelObject.GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
		_uiSlider.value = 0.5f;
		Time.timeScale = _uiSlider.value * 2.0f;

		_uiLabel.text = string.Format("TimeScale : {0}", Time.timeScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isDown) {
//		Debug.Log("ProgressBarTimeScale OnPress()");

		Time.timeScale = _uiSlider.value * 2.0f;

		_uiLabel.text = string.Format("TimeScale : {0:F1}", Time.timeScale);
	}

	void OnClick () {
//		Debug.Log("ProgressBarTimeScale OnClick()");
	}

	void OnHover () {
//		Debug.Log("ProgressBarTimeScale OnHover()");
	}

	void OnMouseOver () {
//		Debug.Log("ProgressBarTimeScale OnMouseOver()");
	}
}
