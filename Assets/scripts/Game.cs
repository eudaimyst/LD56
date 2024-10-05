using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
	private EnemyScript enemyScript;
	private int gameState = 0; //0 = unit selection, 1 = game starting, 2 = in game, 3 = game over
	public GameObject DarkOverlayPanel;
	public GameObject UnitSelectionPanel;
	public TextMeshProUGUI playerResourcesDisplay;
	private int offset = 0; //reusable offset variable
	public GameObject[] bugRef;
	public BugScript[] bugScriptRefs;
	public SpawnScript[] spawners; //used wrong name here it should be bases because spawn point doesnt change
	private BugScript testBug;
	private readonly List<BugScript> spawnedBugs = new();

	private List<BugScript> player1Deck = new();

	private float timer;
	private int[] expansionCount = { 1, 1 };

	private GameObject currentBase;

	public int[] startingResources = { 0, 0 };
	public float resourceGainRate = 40f; //rate per expansion/s
	private float[] resources = { 0f, 0f };

	public UnitSelectionButtonScript[] unitSelectionButtons;
	public UnitSelectionButtonScript[] unitSpawnButtons;

	public int GetGameState()
	{
		return gameState;
	}
	public void SetGameState(int state)
	{
		gameState = state;
	}

	public void GUISpawnBug(int slot) //this is the function called by the UI when button is pressed to spawn bug (for the player only)
	{
		SpawnBug(slot, 0);
	}

	public void GUIAddBugToDeck(int i) //int passed is the bugs position in the bugRef array
	{
		if (player1Deck.Count < 4)
		{
			player1Deck.Add(bugScriptRefs[i]);
			unitSpawnButtons[player1Deck.Count - 1].SetUnit(bugScriptRefs[i]);
		}
		if (player1Deck.Count == 4)
		{
			SetGameState(1); //change game state to game starting
			UnitSelectionPanel.SetActive(false);
			DarkOverlayPanel.SetActive(false);
		}
	}

	private void SpawnBug(int slot, int player)
	{
		BugScript bug = bugRef[slot].GetComponent<BugScript>();
		if (resources[player] >= bug.cost)
		{
			resources[player] -= bug.cost;
			//random between 0 and 1
			testBug = Instantiate(player1Deck[slot], spawners[0].transform.position, Quaternion.identity).GetComponent<BugScript>();
			testBug.transform.position = spawners[0].transform.position;
			spawnedBugs.Add(testBug);
			testBug.SetMoveTarget(spawners[3].gameObject);
		}
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
		resources[0] += resourceGainRate * Time.deltaTime * expansionCount[0];
		resources[1] += resourceGainRate * Time.deltaTime * expansionCount[1];
		playerResourcesDisplay.text = Math.Ceiling(resources[0]).ToString();
	}

	private void UpdateUnitSelectionSprites()
	{
		//Debug.Log(bugScriptRef);
		for (int i = 0; i < bugScriptRefs.Length; i++)
		{
			if (bugScriptRefs[i] != null && unitSelectionButtons[i] != null)
			{
				unitSelectionButtons[i].SetUnit(bugScriptRefs[i]);
			}
			//.SetUnit(bugScriptRef[i]); -testing
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		//for each game object in bugref get the bugscript component and store it in bugscriptref
		/**
		for (int i = 0; i < bugRef.Length; i++)
		{
			bugScriptRef.Add(bugRef[i].GetComponent<BugScript>());
		}
		**/
		//UpdateUnitSelectionSprites();
		resources[0] = startingResources[0];
		resources[1] = startingResources[1];
		//playerResourcesDisplayText = playerResourcesDisplay.GetComponent<TextMeshPro>();
		// Update is called once per frame
	}

	private bool firstFrame = true;
	void Update()
	{
		//print something to the console

		ResourceTick();
		timer += Time.deltaTime;
		if (timer > 1)
		{
			//SpawnBug();
			timer -= 1;
			//Debug.Log(timer);
			enemyScript.TestSpawnEnemy();
		}
		if (firstFrame)
		{
			UpdateUnitSelectionSprites(); //this was failing when put in Start when built (?)
			firstFrame = false;
		}
	}
}