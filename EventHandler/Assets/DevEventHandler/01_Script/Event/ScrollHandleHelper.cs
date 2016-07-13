using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollHandleHelper : MonoBehaviour, IScrollHandler {

	public bool _isScrollScale = false;

	// Method

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnScroll (PointerEventData eventData)
	{
		// use mouse
		Debug.Log ("Call OnScroll()");

		if (_isScrollScale == true)
		{
			const float MinScaleValue = 0.1f;
			float scrollDelta = eventData.scrollDelta.y * 0.1f;

			Vector3 localScale = this.transform.localScale;
			if (localScale.x + scrollDelta > MinScaleValue)
			{
				localScale.x += scrollDelta;
			}

			if (localScale.y + scrollDelta > MinScaleValue)
			{
				localScale.y += scrollDelta;
			}

			if (localScale.z + scrollDelta > MinScaleValue)
			{
				localScale.z += scrollDelta;
			}

			this.transform.localScale = localScale;
		}
	}
}
