using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

	protected static TurnManager _instance = null;
	protected static object	_lock = new System.Object();
	public static TurnManager instance
	{
		get
		{
			if (_instance == null)
			{
				lock (_lock)
				{
					_instance = FindObjectOfType<TurnManager>();
				}
			}

			return _instance;
		}
	}

	public List<TurnActor> _turnActorList = new List<TurnActor>();

	public int _curTurn = 0;

	public UILabel _labelTurnCount = null;

	void Awake () {

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddTurnActor(TurnActor turnActor)
	{
		if (turnActor == null)
			return;

		bool isExist = _turnActorList.Exists(delegate(TurnActor turnActorInList) {
			if (turnActorInList == null)
				return false;

			if (turnActorInList != turnActor)
				return false;

			return true;
		});

		if (isExist == true)
			return;

		_turnActorList.Add(turnActor);
	}

	public void RemoveTurnActor(TurnActor turnActor)
	{
		if (turnActor == null)
			return;

		_turnActorList.Remove(turnActor);
	}

	public void TurnIncrease()
	{
		_curTurn++;
		_labelTurnCount.text = _curTurn.ToString();
	}
}
