using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {

	static EffectManager _instance = null;
	public static EffectManager instance 
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<EffectManager>();

			return _instance;
		}
	}

	// Method

	void Awake () {
		if (_instance == null)
			_instance = this;
	}

	// Use this for initialization
	void Start () {

	}

	void OnEnable () 
	{
		TouchManager.instance._actionTouch += PlayEffect;
	}

	void OnDisable ()
	{
		if (TouchManager.instance)
			TouchManager.instance._actionTouch -= PlayEffect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayEffect(Vector3 pos)
	{
		Debug.Log ("EffectManager.PlayEffect()");

		GameObject effectObj = EffectObjectPool.instance.GetEffect(EffectType.Flare);
		if (effectObj == null)
			return;

		effectObj.transform.position = pos;
		effectObj.SetActive(true);
		StartCoroutine("UnactiveEffect", effectObj);
	}

	IEnumerator UnactiveEffect(GameObject effectObj)
	{
		yield return new WaitForSeconds (60.0f);

		effectObj.SetActive(false);

		yield break;
	}
}
