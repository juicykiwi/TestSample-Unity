using UnityEngine;
using System.Collections;

public enum TurnActorState
{
	None,
	Wait,
	Ended,
};

public class TurnActor : MonoBehaviour {

	TurnActorState _turnActorState = TurnActorState.None;

	void Awake() {

	}

	// Use this for initialization
	void Start () {

	}

	void OnEnable () {
		TurnManager.instance.AddTurnActor(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable () {
		/*
		 * TurnManager의 오브젝트가 제거되고 나서 여기 함수가 호출 될 수 있어서 안전하게 예외 처리 코드 추가
		 */
		if (TurnManager.instance)
		{
			TurnManager.instance.RemoveTurnActor(this);
		}
	}
}
