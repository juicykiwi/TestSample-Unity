using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaskCircle : MonoBehaviour {

	public Material _maskMaterial = null;

	public Vector2 _cutPos = Vector2.zero;
	public float _cutRadius = 0.0f;
	public float _gradientRadius = 0.0f;

	bool _isExpandGradient = false;
	public float _gradientSpeed = 0.0f;
	public float _gradientRadiusMin = 0.0f;
	public float _gradientRadiusMax = 0.0f;

	Image _image = null;
	RectTransform _rectTransform = null;

	// Method

	void Awake () {
		_image = this.GetComponent<Image>();
		_rectTransform = this.GetComponent<RectTransform>();

		_image.material = _maskMaterial;
	}

	// Use this for initialization
	void Start () {
		SetMaterialCutPos();
		SetMaterialCutRadius();
		SetMaterialGradientRadius();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isExpandGradient == false)
		{
			_gradientRadius += _gradientSpeed * Time.deltaTime;

			if (_gradientRadius >= _gradientRadiusMax)
			{
				_isExpandGradient = true;
			}
		}
		else
		{
			_gradientRadius -= _gradientSpeed * Time.deltaTime;

			if (_gradientRadius <= _gradientRadiusMin)
			{
				_isExpandGradient = false;
			}
		}

		SetMaterialCutPos();
		SetMaterialCutRadius();
		SetMaterialGradientRadius();
	}

	public void SetMaterialCutPos()
	{
		Vector4 cutPos = new Vector4(_cutPos.x, _cutPos.y, 0.0f, 0.0f);
		_maskMaterial.SetVector("_CutScreenPos", cutPos);
	}

	public void SetMaterialCutRadius()
	{
		float curRadius = _cutRadius;
		_maskMaterial.SetFloat("_CutRadius", curRadius);
	}

	public void SetMaterialGradientRadius()
	{
		float gradientRadius = _gradientRadius;
		_maskMaterial.SetFloat("_GradientRadius", gradientRadius);
	}
}
