using UnityEngine;
using System.Collections;

public class AISpawn : AIBase {

	public float _spawnTime = 1.0f;

	protected override void Awake () {
		base.Awake();

		_aiState = AIState.Spawn;
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
			if (_stateRunTime >= _spawnTime)
			{
				_aiController.SetAIState(AIState.Idle);
				break;
			}
			
			yield return new WaitForSeconds(_aiUpdateTime);
		} while (isLoop);

		yield return null;
	}
}
