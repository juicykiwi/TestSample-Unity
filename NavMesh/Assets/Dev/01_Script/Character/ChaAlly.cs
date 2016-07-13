using UnityEngine;
using System.Collections;

public class ChaAlly : MonoBehaviour {

	public Vector3 targetPos = Vector3.zero;

	private NavMeshAgent _agent = null;

	void Awake () {
		targetPos = gameObject.transform.position;

		_agent = this.GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_agent.SetDestination(targetPos);
	}

	public void SetTarget(Transform target)
	{
		this.targetPos = target.position;
	}
}
