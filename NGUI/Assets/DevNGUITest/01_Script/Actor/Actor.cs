using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	protected AIController _aiController = null;

	protected ActorImageController _imageController = null;
	public ActorImageController GetActorImageController() { return _imageController; }

	protected TextState _textState = null;
	public TextState GetTextState() { return _textState; }

	protected TurnActor _turnActor = null;
	public TurnActor GetTurnActor() { return _turnActor; }

	public float _limitMoveInTurn = 1.0f;
	public float _limitScreenMoveInTurn = 120.0f;

	protected Vector3 _destPosInWorld = Vector3.zero;
	public Vector3 destPosInWorld
	{
		set
		{
			Vector3 curPosition = this.transform.position;
			Vector3 destPosition = value;
			Vector3 direction = Vector3.Normalize(destPosition - curPosition);
			float distance = Vector3.Distance(curPosition, destPosition);

			if (distance > _limitMoveInTurn)
			{
				destPosition = curPosition + direction * _limitMoveInTurn;
			}

			Rect rectBattleGround = BattleGround.GetInstance()._rectInWorld;

			float destPosX = 0.0f;
			destPosX = Mathf.Max(destPosition.x, rectBattleGround.x);
			destPosX = Mathf.Min(destPosX, rectBattleGround.x + rectBattleGround.width);
			
			float destPosY = 0.0f;
			destPosY = Mathf.Max(destPosition.y, rectBattleGround.y);
			destPosY = Mathf.Min(destPosY, rectBattleGround.y + rectBattleGround.height);
			
			_destPosInWorld = new Vector3(destPosX, destPosY, 0.0f);
		}
		get
		{
			return _destPosInWorld;
		}
	}

	protected Vector2 _destPosInScreen = Vector2.zero;
	public Vector2 destPosInScreen
	{
		set
		{
			Vector2 curPosition = UICamera.mainCamera.WorldToScreenPoint(this.transform.position);
			Vector2 destPosition = value;
			Vector2 direction = (destPosition - curPosition).normalized;
			float distance = Vector2.Distance(curPosition, destPosition);

			if (distance > _limitScreenMoveInTurn)
			{
				destPosition = curPosition + direction * _limitMoveInTurn;
			}

			Rect rectBattleGround = BattleGround.GetInstance()._rect;

			float destPosX = 0.0f;
			destPosX = Mathf.Max(value.x, rectBattleGround.x);
			destPosX = Mathf.Min(destPosX, rectBattleGround.x + rectBattleGround.width);

			float destPosY = 0.0f;
			destPosY = Mathf.Max(value.y, rectBattleGround.y);
			destPosY = Mathf.Min(destPosY, rectBattleGround.y + rectBattleGround.height);

			_destPosInScreen = new Vector2(destPosX, destPosY);
		}
		get
		{
			return _destPosInScreen;
		}
	}

	public float _speed = 1.0f;

	void Awake () {
		_aiController = this.GetComponentInChildren<AIController>();
		_imageController = this.GetComponentInChildren<ActorImageController>();
		_textState = this.GetComponentInChildren<TextState>();
		_turnActor = this.GetComponentInChildren<TurnActor>();

		Vector3 screenPoint = UICamera.mainCamera.WorldToScreenPoint (this.transform.position);
		_destPosInScreen = new Vector2 (screenPoint.x, screenPoint.y);
		_destPosInWorld = this.transform.position;
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("LogActorRelated", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		ActorMoveInWorld();
	}

	void ActorMoveInWorld()
	{
		if (_aiController._aiState != AIState.Move)
			return;

		Vector3 direction = Vector3.Normalize(_destPosInWorld - this.transform.position);
		float distance = Vector3.Distance (_destPosInWorld, this.transform.position);

		if (distance > _speed * Time.deltaTime)
		{
			this.transform.position = this.transform.position + (direction * _speed * Time.deltaTime);
		}
		else
		{
			this.transform.position = this.transform.position + (direction * distance);
		}
	}

	void ActorMoveInScreen()
	{
		if (_aiController._aiState != AIState.Move)
			return;
		
		Vector3 screenDestPos = new Vector3(_destPosInScreen.x, _destPosInScreen.y, 0.0f);
		Vector3 worldDestPos = UICamera.mainCamera.ScreenToWorldPoint(screenDestPos);
		worldDestPos.z = 0.0f;

		Vector3 direction = Vector3.Normalize(worldDestPos - this.transform.position);
		float distance = Vector3.Distance (worldDestPos, this.transform.position);
		
		if (distance > _speed * Time.deltaTime)
		{
			this.transform.position = this.transform.position + (direction * _speed * Time.deltaTime);
		}
		else
		{
			this.transform.position = this.transform.position + (direction * distance);
		}
	}

	public bool IsMovingInWorld()
	{
		if (Vector3.Distance(destPosInWorld, this.transform.position) < 0.1f)
		{
			return false;
		}

		return true;
	}

	public bool IsMovingInScreen()
	{
		Vector3 screenPoint = UICamera.mainCamera.WorldToScreenPoint(this.transform.position);
		screenPoint.z = 0.0f;

		if (Vector3.Distance(destPosInScreen, screenPoint) < 1.0f)
		{
			return false;
		}

		return true;
	}

	public Vector2 GetScreenPosition()
	{
		return UICamera.mainCamera.WorldToScreenPoint(this.transform.position);
	}

	void LogActorRelated()
	{
		// current world pos
//		Vector3 curWorldPos = this.transform.position;

		// current screen pos
//		Vector3 curScreenPos = UICamera.mainCamera.WorldToScreenPoint (this.transform.position);

		// dest screen pos
//		Vector3 destScreenPos = new Vector3(_destPosInScreen.x, _destPosInScreen.y, 0.0f);

		// dest world pos
//		Vector3 destWorldPos = UICamera.mainCamera.ScreenToWorldPoint(destScreenPos);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (_aiController.GetAIState() == AIState.Die)
			return;

		Actor otherActor = collider.GetComponent<Actor>();
		if (otherActor == null)
			return;

		if (otherActor._aiController.GetAIState() == AIState.Die)
			return;

		_aiController.SetAIState(AIState.Die);
		_imageController.ChangeImage(_aiController.GetAIState());
		_imageController.HideImageDie();

		otherActor._aiController.SetAIState(AIState.Die);
		otherActor._imageController.ChangeImage(otherActor._aiController.GetAIState());
		otherActor._imageController.HideImageDie();
	}
}
