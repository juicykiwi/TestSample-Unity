using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MoveHandleHelper : MonoBehaviour, IMoveHandler {

	public UnityEvent _event = null;

	EventTrigger _eventTrigger = null;
	public EventTrigger eventTrigger
	{
		get
		{
			if (_eventTrigger == null)
			{
				_eventTrigger = this.GetComponent<EventTrigger>();
			}

			return _eventTrigger;
		}
	}

	// Method

	void Awake () {
		_eventTrigger = this.GetComponent<EventTrigger>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.hasChanged == true)
		{
			AxisEventData axisEventData = new AxisEventData(EventSystem.current);
			axisEventData.moveVector = transform.position;

			ExecuteEvents.Execute<IMoveHandler>(this.gameObject,
			                                    axisEventData,
			                                    (Handler, data) => Handler.OnMove((AxisEventData)data));

			transform.hasChanged = false;
		}
	}

	public void DoMove(Vector3 moveDelta)
	{
		this.transform.Translate(moveDelta);
	}

	public void OnMove (AxisEventData eventData)
	{
		Debug.Log("Call OnMove()");
	}
}

#if UNITY_EDITOR

[CanEditMultipleObjects]
[CustomEditor(typeof(MoveHandleHelper))]
public class MoveHandleHelperEditor : Editor
{
	public override void OnInspectorGUI ()
	{
//		base.OnInspectorGUI ();

		DrawDefaultInspector();

		MoveHandleHelper moveHandleHelper = (MoveHandleHelper)this.target;

		// 1 row

		GUILayout.BeginHorizontal(GUILayout.MaxWidth(60f));
		{
			GUILayout.Space(20f);

			if (GUILayout.Button("U") == true)
			{
				moveHandleHelper.DoMove(new Vector3(0f, 1f, 0f));
			}

			GUILayout.Space(20f);
		}
		GUILayout.EndHorizontal();

		// 2 row

		GUILayout.BeginHorizontal(GUILayout.MaxWidth(60f));
		{
			if (GUILayout.Button("L") == true)
			{
				moveHandleHelper.DoMove(new Vector3(-1f, 0f, 0f));
			}

			GUILayout.Space(20f);
			
			if (GUILayout.Button("R") == true)
			{
				moveHandleHelper.DoMove(new Vector3(1f, 0f, 0f));
			}
		}
		GUILayout.EndHorizontal();

		// 3 row

		GUILayout.BeginHorizontal(GUILayout.MaxWidth(60f));
		{
			GUILayout.Space(20f);
			
			if (GUILayout.Button("D") == true)
			{
				moveHandleHelper.DoMove(new Vector3(0f, -1f, 0f));
			}
			
			GUILayout.Space(20f);
		}
		GUILayout.EndHorizontal();
	}
}

#endif
