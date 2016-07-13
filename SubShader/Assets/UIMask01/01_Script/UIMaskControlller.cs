using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaskControlller : MonoBehaviour {

	public Image _maskImage = null;

	public Vector4 _cutPos = Vector4.zero;
	public float _cutRadius = 1.0f;
	public float _gradientLenght = 1.0f;

	public bool _isActiveMask = false;

	Material _maskMaterial = null;

	// Method

	void Awake () {
		_maskMaterial = _maskImage.material;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (_isActiveMask == true)
		{
			_maskImage.gameObject.SetActive(true);

			_maskMaterial.SetVector("_CutPos", _cutPos);
			_maskMaterial.SetFloat("_Radius", _cutRadius);
			_maskMaterial.SetFloat("_Gradient", _gradientLenght);
		}
		else
		{
			_maskImage.gameObject.SetActive(false);
		}
	}
}
