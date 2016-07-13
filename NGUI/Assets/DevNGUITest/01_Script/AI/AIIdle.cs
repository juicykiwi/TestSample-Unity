using UnityEngine;
using System.Collections;

public class AIIdle : AIBase {

	public float _waitTime = 1.0f;

	protected override void Awake () {
		base.Awake();

		_aiState = AIState.Idle;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	protected override void OnEnable () {
		base.OnEnable();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	protected override void OnDisable () {
		base.OnDisable();
	}

	protected override IEnumerator CoroutineUpdateAI()
	{
		bool isLoop = true;
		
		do
		{
			if (_stateRunTime >= _waitTime)
			{
				_aiController.SetAIState(AIState.Move);
				break;
			}
			
			yield return new WaitForSeconds(_aiUpdateTime);
		} while (isLoop);

		yield return null;
	}
}
