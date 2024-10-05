using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionButtonScript : MonoBehaviour
{
	private TextMeshProUGUI nameText;
	private Image imageComponent;

	public void SetUnit(BugScript bug)
	{
		// Set the name and icon of the unit
		// This is called by the UnitSelectionScript
		Debug.Log("loading " + bug.name);
		nameText.text = bug.name;
		imageComponent.sprite = bug.icon;
	}
	// Start is called before the first frame update
	void Start()
	{
		imageComponent = this.GetComponent<Image>();
		nameText = this.GetComponentInChildren<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
