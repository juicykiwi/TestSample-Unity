using UnityEngine;
using System.Collections;

public class AIMove : AIBase {

	protected override void Awake () {
		base.Awake();

		_aiState = AIState.Move;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	protected override void OnEnable () {
		SetDestPoisInWorld();

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
			if (_aiController._actor.IsMovingInWorld() == false)
			{
				_aiController.SetAIState(AIState.Idle);
				break;
			}
			
			yield return new WaitForSeconds(_aiUpdateTime);
		} while (isLoop);

		yield return null;
	}

	void SetDestPoisInScreen()
	{
		float distance = Vector2.Distance (_aiController._actor.destPosInScreen, _aiController._actor.GetScreenPosition());
		if (distance <= 1.0f)
		{
			Rect rectBattleGround = BattleGround.GetInstance()._rect;

			float destPosX = Random.Range(rectBattleGround.x, rectBattleGround.x + rectBattleGround.width);
			float destPosY = Random.Range(rectBattleGround.y, rectBattleGround.y + rectBattleGround.height);

			_aiController._actor.destPosInScreen = new Vector2(destPosX, destPosY);
		}
	}

	void SetDestPoisInWorld()
	{
		float distance = Vector2.Distance (_aiController._actor.destPosInWorld, _aiController._actor.transform.position);
		if (distance <= 0.1f)
		{
			Rect rectBattleGround = BattleGround.GetInstance()._rectInWorld;
			float destPosX = Random.Range(rectBattleGround.x, rectBattleGround.x + rectBattleGround.width);
			float destPosY = Random.Range(rectBattleGround.y, rectBattleGround.y + rectBattleGround.height);
			
			_aiController._actor.destPosInWorld = new Vector3(destPosX, destPosY, 0.0f);
		}
	}
}
