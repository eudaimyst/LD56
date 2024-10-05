using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{

	Vector3 moveTarget;
	public float cost;
	public float hp;
	public float speed;
	public float range;
	public float attack;
	public float attackSpeed;
	public bool flying;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (moveTarget != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
		}
	}

	public bool SetMoveTarget(GameObject target)
	{
		if (target.transform.position != null)
		{
			moveTarget = target.transform.position;
			//Debug.Log("set move target for " + this.gameObject.name + "to" + moveTarget);
			return true;
		}
		else
		{

			//Debug.Log("failed set move target for " + this.gameObject.name);
			return false;
		}
	}
}
