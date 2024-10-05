using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
	public TextMeshProUGUI playerResourcesDisplay;
	private int offset = 0; //reusable offset variable
	public GameObject[] bugRef;
	public SpawnScript[] spawners; //used wrong name here it should be bases because spawn point doesnt change
	private BugScript testBug;
	private readonly List<BugScript> testBugs = new();

	private float timer;
	private int[] expansionCount = { 1, 1 };

	private GameObject currentBase;

	public int[] startingResources = { 0, 0 };
	public int resourceGainRate = 40; //rate per expansion
	private int[] resources = { 0, 0 };

	public void SpawnBug(int slot)
	{
		//random between 0 and 1
		if (Math.Round(UnityEngine.Random.Range(0f, 1f)) == 0)
		{
			slot += 4;
		}
		testBug = Instantiate(bugRef[slot], spawners[0].transform.position, Quaternion.identity).GetComponent<BugScript>();
		testBug.transform.position = spawners[0].transform.position;
		testBugs.Add(testBug);
		testBug.SetMoveTarget(spawners[3].gameObject);
	}

	public void BuildExpansion(int player)
	{
		if (player == 1) offset = 3;
		else offset = 0;


		if (offset + expansionCount[player] < 3)
		{
			currentBase = spawners[offset + expansionCount[player]].gameObject;
			currentBase.transform.Find("flowerbed").gameObject.SetActive(true);
			expansionCount[player] += 1;
		}
	}

	private void ResourceTick()
	{
		resources[0] += resourceGainRate * expansionCount[0];
		resources[1] += resourceGainRate * expansionCount[1];
		playerResourcesDisplay.text = resources[0].ToString();
	}
	// Start is called before the first frame update
	void Start()
	{
		//playerResourcesDisplayText = playerResourcesDisplay.GetComponent<TextMeshPro>();
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
			ResourceTick();
			//Debug.Log(timer);
		}
	}
}