using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour {

	protected AIState _aiState = AIState.None;
	public AIState GetAiState() { return _aiState; }

	public float _aiUpdateTime = 1.0f;

	protected float _stateRunTime = 0.0f;

	protected AIController _aiController = null;

	protected virtual void Awake () {
		_aiController = this.GetComponent<AIController>();
	}

	// Use this for initialization
	protected virtual void Start () {

	}

	protected virtual void OnEnable () {
		_stateRunTime = 0.0f;
		StartCoroutine("CoroutineUpdateAI");
	}

	// Update is called once per frame
	protected virtual void Update () {
		_stateRunTime += Time.deltaTime;
	}

	protected virtual void OnDisable () {

	}

	protected virtual IEnumerator CoroutineUpdateAI()
	{
		bool isLoop = true;

		do
		{
			yield return new WaitForSeconds(_aiUpdateTime);
		} while (isLoop);

		yield return null;
	}
}
