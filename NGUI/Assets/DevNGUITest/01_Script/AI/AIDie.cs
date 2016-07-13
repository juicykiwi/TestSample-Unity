using UnityEngine;
using System.Collections;

public class AIDie : AIBase {

	protected override void Awake() {
		base.Awake();

		_aiState = AIState.Die;
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
			if (_aiController._actor.GetActorImageController().IsHideComplete() == true)
			{
				_aiController._actor.GetActorImageController().CancelInvoke();
				_aiController._actor.GetTextState().gameObject.SetActive(false);
			}

			yield return new WaitForSeconds(_aiUpdateTime);
		} while (isLoop);

		yield return null;
	}
}
