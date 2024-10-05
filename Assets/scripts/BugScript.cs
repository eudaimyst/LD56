using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
	public float col_radius;
	public Sprite icon;

	private SphereCollider col;

	// Start is called before the first frame update
	void Start()
	{
		if (col_radius == 0f) col_radius = 3f;
		col = this.AddComponent<SphereCollider>();
		col.center = this.transform.GetChild(0).transform.localPosition;
		col.radius = col_radius;
		Debug.Log("set col radius to " + col_radius + " for " + this.name);

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
			Debug.Log("failed set move target for " + this.gameObject.name);
			return false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "unit")
		{
			foreach (ContactPoint contact in collision.contacts)
			{
				Debug.DrawRay(contact.point, contact.normal, Color.red);
			}
			Debug.Log("Collision detected between " + this.gameObject.name + " and " + collision.gameObject.name);
		}
	}
}
