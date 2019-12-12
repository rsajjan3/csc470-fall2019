using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.IO;


//Navmesh to have movement be smooth
public class GameManager : MonoBehaviour
{
	// This will hold a reference to whichever Unit was selected last.
	UnitScript selectedUnit;

	// References to a handful of UI elements.
	public ToggleGroup actionSelectToggleGroup;
	public GameObject selectedPanel;
	public Text nameText;

	// Set the fillAmount of this Image to a value between 0 and 1 to set the meter.
	public Image meterFG;
	// We will move this object around and turn it on and off (done in UnitScript OnMouse function).
	public GameObject timerObject;
	public GameObject explosion;
	public GameObject beerCan;
	public AudioSource audioSource;
	private AudioClip[] soundClips;
	private GameObject[] cubeObjs;
	private GameObject[] moveCubess;
	private bool canMove;

	private int attackCount;
	private void playAudioClip()
	{
		audioSource.clip = soundClips[Random.Range(0,soundClips.Length)];
		audioSource.Play();
	}

	// Start is called before the first frame update
	void Start()
	{
		attackCount = 0;

		soundClips = Resources.LoadAll<AudioClip>("Sounds");
		playAudioClip();

		cubeObjs = GameObject.FindGameObjectsWithTag("cube");
		canMove = true;
	}
	void attack()
	{
		moveCubes();
		attackCount++;
		if(attackCount % 10 == 0) playAudioClip(); //Play audio clip every 10 attacks
        Vector3 position = selectedUnit.transform.position + selectedUnit.transform.forward;
        GameObject can = Instantiate(beerCan, position, selectedUnit.transform.rotation);
        Destroy(can, 3);
	}

	void moveCubes() //Can make map more difficult or easier to move arround
	{
		if(canMove)
		{
			moveCubess = new GameObject[10];
			for(int i = 0; i < moveCubess.Length; i++)
			{
				moveCubess[i] = cubeObjs[Random.Range(0, cubeObjs.Length)];
				moveCubess[i].transform.position = moveCubess[i].transform.position + new Vector3(0, 5, 0); //Move cube 5 units up
			}
			canMove = false;
		}
		else
		{
			for(int i = 0; i < moveCubess.Length; i++)
			{
				moveCubess[i].transform.position = moveCubess[i].transform.position - new Vector3(0, 5, 0); //Move cube 5 units down
			}
			canMove = true;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			attack();
		}
		// Input.GetMouseButtonDown(0) is how you detect that the left mouse button has been clicked.
		//
		// The IsPointerOverGameObject makes sure the pointer is over the UI. In this case,
		// we don't want to register clicks over the UI when determining what unit is 
		// selected or deselected.
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
		{
			// Create a ray from the mouse position (in camera/ui space) to 3d world space.
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// After the Raycast, 'hit' will store information about what the raycast hit.
			RaycastHit hit;
			// The line below actually performs the "raycast". This will 'shoot' a line from the
			// mouse position into the world, and it if hits something marked with the layer 'ground', 
			// return true.
			if (Physics.Raycast(ray, out hit, 9999)) 
			{
				// Check to see if the thing the raycast hit was marked with the layer 'ground'.
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ground")) 
				{
					// If so, set the destination of the selectedUnit to the point on the ground
					// that the raycast hit.
					if (selectedUnit != null) 
					{
						selectedUnit.destination = hit.point;
					}
				}
			} 
			else 
			{
				// If we got here, it means that the raycast didn't hit anything, so let's deselect.
				if (selectedUnit != null) {
					selectedUnit.selected = false;
					selectedUnit = null;

					updateSelectedPanelUI();
				}
			}
		}
	}

	public void selectUnit(UnitScript unit)
	{
		// If we have selected something previously, unselect it and update the color.
		if (selectedUnit != null) {
			selectedUnit.selected = false;
		}

		// Set selected unit to the one we just passed in.
		selectedUnit = unit;

		if (selectedUnit != null) {
			// If there is a selected unit, update its color.
			selectedUnit.selected = true;
		}

		updateSelectedPanelUI();
	}

	// This function updates the UI elements based on what was clicked on.
	void updateSelectedPanelUI()
	{
		// Only update the UI is there is a unit selected.
		if (selectedUnit != null) {
			nameText.text = selectedUnit.name;
			selectedPanel.SetActive(true);
		} else {
			// If there is no selected unit, turn the panel off.
			selectedPanel.SetActive(false);
		}
	}

	// This function is called by the EventSystem when the player clicks on the PerformActionButton.
	public void TakeAction()
	{
		// Figure out which toggle button is selected in the action select toggleGroup
		// and store the text value of the button in a string.
		IEnumerable<Toggle> activeToggles = actionSelectToggleGroup.ActiveToggles();
		string action = "";
		foreach (Toggle t in activeToggles) {
			if (t.IsActive()) {
				action = t.gameObject.GetComponentInChildren<Text>().text;
			}
		}

		if(action == "Talk")
		{
			playAudioClip();
		}
		else if (action == "Attack")
		{
			attack();
		}
	}
}