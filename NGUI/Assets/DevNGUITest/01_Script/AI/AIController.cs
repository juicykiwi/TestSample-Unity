using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AIState
{
	None 	= 0,
	Spawn	= 1,
	Idle	= 2,
	Move	= 3,
	Die		= 4,
}

public class AIController : MonoBehaviour {

	public AIState _aiState = AIState.Spawn;
	public AIState GetAIState() { return _aiState; }

	public List<AIBase> _aiList = new List<AIBase>();

	public Dictionary<AIState, AIBase> _dictAI = new Dictionary<AIState, AIBase>();

	public Actor _actor = null;

	void Awake () {
		_actor = this.GetComponentInParent<Actor>();
	}

	// Use this for initialization
	void Start () {
		foreach (AIBase ai in _aiList)
		{
			if (ai == null)
				continue;
			
			if (_dictAI.ContainsKey(ai.GetAiState()) == true)                                
				continue;

			ai.enabled = false;
			_dictAI.Add(ai.GetAiState(), ai);
		}
		
		SetAIState(AIState.Spawn);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetAIState(AIState aiState)
	{
		if (_dictAI[aiState] == null)
			return;

		if (_dictAI.ContainsKey(_aiState) == true)
			_dictAI[_aiState].enabled = false;

		_aiState = aiState;
		_dictAI[_aiState].enabled = true;

		_actor.GetTextState()._labelState.text = _aiState.ToString();
	}
}
