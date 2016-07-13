using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour {

	public Image _image = null;

	// Method

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeRandomColorForImage()
	{
		if (_image == null)
			return;

		_image.color = new Color(Random.Range(0f, 1f), 
		                        Random.Range(0f, 1f),
		                        Random.Range(0f, 1f),
		                        Random.Range(0f, 1f));
	}
}
