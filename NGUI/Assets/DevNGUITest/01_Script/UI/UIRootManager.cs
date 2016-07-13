using UnityEngine;
using System.Collections;

public class UIRootManager : MonoBehaviour {

	protected static UIRootManager _instance = null;
	public static UIRootManager GetInstance() { return _instance; }

	protected UIRoot _uiRoot = null;

	public float _screenWidth = 0.0f;
	public float _screenHeight = 0.0f;

	public float _contentWidth = 0.0f;
	public float _contentHeight = 0.0f;

	public float _resolutionScaleWidth = 0.0f;
	public float _resolutionScaleHeight = 0.0f;

	void Awake () {
		_instance = this;

		_uiRoot = GetComponent<UIRoot>();

		_screenWidth = Screen.width;
		_screenHeight = Screen.height;

		_contentWidth = _uiRoot.manualWidth;
		_contentHeight = _uiRoot.manualHeight;

		_resolutionScaleWidth = (float)Screen.width / (float)_uiRoot.manualWidth;
		_resolutionScaleHeight = (float)Screen.height / (float)_uiRoot.manualHeight;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 PixelToScreenPoint(Vector2 pixelPoint)
	{
		Vector2 screenPoint = new Vector2();

		if (_resolutionScaleWidth > 0.0f)
			screenPoint.x = pixelPoint.x * _resolutionScaleWidth;

		if (_resolutionScaleHeight > 0.0f)
			screenPoint.y = pixelPoint.y * _resolutionScaleHeight;

		return screenPoint;
	}

	public Vector2 ScreenToPixelPoint(Vector2 screenPoint)
	{
		Vector2 pixelPoint = new Vector2();
		
		if (_resolutionScaleWidth > 0.0f)
			screenPoint.x = pixelPoint.x / _resolutionScaleWidth;
		
		if (_resolutionScaleHeight > 0.0f)
			screenPoint.y = pixelPoint.y / _resolutionScaleHeight;
		
		return pixelPoint;
	}

	public Vector3 ScreenToWorldSize(Vector2 screenSize)
	{
		Vector3 worldPointZero = UICamera.mainCamera.ScreenToWorldPoint(Vector3.zero);
		Vector3 WorldPoint = UICamera.mainCamera.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 0.0f));

		Vector3 worldSize = Vector3.zero;
		worldSize.x = WorldPoint.x - worldPointZero.x;
		worldSize.y = WorldPoint.y - worldPointZero.y;

		return worldSize;
	}
}
