using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum EffectType
{
	None,
	Flare,
}

public class EffectObjectPool : MonoBehaviour {

	static EffectObjectPool _instance = null;
	public static EffectObjectPool instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<EffectObjectPool>();

			return _instance;
		}
	}

	Dictionary<EffectType, List<GameObject>> _effectPool = new Dictionary<EffectType, List<GameObject>>();

	// Method

	void Awake () {
		if (_instance == null)
			_instance = this;

		PushEffect(EffectType.Flare, 40);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PushEffect(EffectType effectType, int count)
	{
		GameObject prefabObj = null;

		switch (effectType)
		{
		case EffectType.Flare:
			prefabObj = Resources.Load<GameObject>("Prefabs/Liberate_03");
			break;

		default:
			break;
		}

		if (prefabObj == null)
		{
			Debug.Log("EffectObjectPool.PushEffect() : prefabObj is null.");
			return;
		}

		List<GameObject> effectList = new List<GameObject>();

		for (int i = 0; i < count; ++i)
		{
			GameObject effectObj = Instantiate<GameObject>(prefabObj);
			if (effectObj == null)
				break;

			effectObj.SetActive(false);
			effectList.Add(effectObj);
		}

		_effectPool.Add(effectType, effectList);
	}

	public GameObject GetEffect(EffectType type)
	{
		if (_effectPool.ContainsKey(type) == false)
			return null;

		List<GameObject> effectObjList = _effectPool[type];
		return effectObjList.Find(
			(GameObject obj) => { return obj.activeSelf == false; });
	}
}
