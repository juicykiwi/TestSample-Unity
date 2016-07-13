using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public const float DefaultOrthographicSize = 5f;

	public int DefaultScreenWidth = 1136;
	public int DefaultScreenHeight = 640;
	public bool isFullScreen = false;

	static CameraManager _instance = null;
	public static CameraManager instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<CameraManager>();

			return _instance;
		}
	}

	Camera _mainCamera = null;

	// Method

	void Awake () {

		_instance = this;

		_mainCamera = Camera.main;	// Main Camera has "Main Camare" Tag.
	}

	// Use this for initialization
	void Start () {
		/* OrthorgraphicResize()보다 SetResolution() 방식이
		 * 시스템에 맏다고 생각하여 사용
		 * 작업이 마무리 되었을 시 어는 것이 맞는지 또는 수정이 필요한지 확인 필요
		 */ 

		SetResolution();
//		OrthographicResize();
	}
	
//	 public override void ActionSceneLoaded(SceneType sceneType)
//	 public override void ActionSceneClosed(SceneType sceneType)

	public void SetResolution()
	{
		// 기본 설정으로 카메라 화면비 설정 및 출력 Rect 변경

		// Set Aspect

		Camera.main.aspect = (float)DefaultScreenWidth / (float)DefaultScreenHeight; // 1.775

		// Set Viewport rect

		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		
		float ratioDefaultWidth = (float)screenWidth / (float)DefaultScreenWidth;
		float ratioDefaultHeight = (float)screenHeight / (float)DefaultScreenHeight;

		float currendScreenAspect = (float)screenWidth / (float)screenHeight; // 1.3333

		if (isFullScreen == false)
		{
			if (Camera.main.aspect >= currendScreenAspect)
			{
				// Width 중심으로 Height 세팅
				float ratioCurrentHeight = ratioDefaultWidth / ratioDefaultHeight;
				Camera.main.rect = new Rect (0f, (1f - ratioCurrentHeight) / 2f, 1f, ratioCurrentHeight);
			}
			else
			{
				// Height 중심으로 Width 세팅
				float ratioCurrentWidth = ratioDefaultHeight / ratioDefaultWidth;
				Camera.main.rect = new Rect ((1f - ratioCurrentWidth) / 2f, 0f, ratioCurrentWidth, 1f);
			}
		}
	}

	public void OrthographicResize()
	{
		// 카메라의 OrthographicSize를 변경하는 방식
		// 이 방식은 카메라 Projection이 Othographic 타입일 경우에만 사용 가능하다.

		/* 해상도 및 화면비
		 * 
		 * < iOS >
		 * iphone5s : 1136:640 = 16:9
		 * 4s : 960:640 = 3:2
		 * 6 : 1334:750 = 16:9
		 * 6 Plus : 2208:1242 = 16:9
		 * iPad2 : 1024:768 = 4:3
		 * 
		 * < Android >
		 * Nexus 7-1 : 1280:800 = 16:10
		 */

		// 9:16 세로 가로 비율 = 0.5633f
		float rateWidthHeight = (float)DefaultScreenHeight / (float)DefaultScreenWidth;
		
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		Debug.LogFormat("Screen width:{0}, height:{1}", screenWidth, screenHeight);

		float rateWidth = (float)screenWidth / (float)DefaultScreenWidth;
		float rateHeight = (float)screenHeight / (float)DefaultScreenHeight;

		if (rateWidth <= rateHeight)
		{
			float newScreenHeight = screenWidth * rateWidthHeight;	// 현재 스크린 비율에 대한 높이 값
			float rateOldNewHeight = newScreenHeight / screenHeight;
			
			_mainCamera.orthographicSize = CameraManager.DefaultOrthographicSize / rateOldNewHeight;
		}
		else
		{	
			float newScreenWidth = screenHeight / rateWidthHeight;
			float rateOldNewWidth = newScreenWidth / screenWidth;
			
			_mainCamera.orthographicSize = CameraManager.DefaultOrthographicSize / rateOldNewWidth;
		}
	}
}
