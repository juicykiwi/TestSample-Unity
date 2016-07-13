using UnityEngine;
using System.Collections;

public class ButtonPause : MonoBehaviour {

	bool flag = true;

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
		flag = ! flag;

		if (flag)
		{
			Time.timeScale = 1.0f;
			Debug.Log ("run");
		}
		else
		{
			Time.timeScale = 0.0f;
			Debug.Log ("pause");
		}
	}
}
