using UnityEngine;
using System.Collections;

public class ActorImageController : MonoBehaviour {

	public GameObject _imageAlive = null;
	public GameObject _imageDie = null;

	protected bool _isHideComplete = false;
	public bool IsHideComplete() { return _isHideComplete; }

	protected Animator _animator;

	void Awake () {
		_animator = this.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeImage(AIState ai)
	{
		switch (ai)
		{
		case AIState.Die:
		{
			_imageAlive.SetActive(false);
			_imageDie.SetActive(true);
		}
			break;

		default:
		{
			_imageAlive.SetActive(true);
			_imageDie.SetActive(false);
		}
			break;
		}
	}

	public void HideImageDie()
	{
		_animator.SetBool("IsHide", true);
	}

	public void HideImageDieComplete()
	{
		_isHideComplete = true;
	}
}
