using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
	public GameObject bugRef;
	public SpawnScript[] spawners;
	private BugScript testBug;
	private List<BugScript> testBugs = new List<BugScript>();

	private float timer;


	public void SpawnBug()
	{
		testBug = Instantiate(bugRef, spawners[0].transform.position, Quaternion.identity).GetComponent<BugScript>();
		testBug.transform.position = spawners[0].transform.position;
		testBugs.Add(testBug);
		testBug.SetMoveTarget(spawners[3].gameObject);
	}
	// Start is called before the first frame update
	void Start()
	{

		// Update is called once per frame
	}
	void Update()
	{
		//print something to the console

		timer += Time.deltaTime;
		if (timer > 1)
		{
			//SpawnBug();
			timer -= 1;
			//Debug.Log(timer);
		}
	}
}