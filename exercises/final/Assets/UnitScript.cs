using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitScript : MonoBehaviour
{
	public bool selected;
	public string name;
	public float timeLeft;
	public Vector3 destination;
	public Renderer rend; // NOTE: We set this reference in the prefab editor within the Unity editor.
	private GameManager gm;
	private Color defautColor;
	private CharacterController cc;
	private float initTime;

	// Start is called before the first frame update
	void Start()
	{
		selected = false;
		name = "Hank Hill";
		initTime = Random.Range(45.0f, 90.0f);
		timeLeft = initTime;

		GameObject gmObj = GameObject.Find("GameManagerObject");
		gm = gmObj.GetComponent<GameManager>();

		// Get a reference to the CharacterController component on the gameObject (i.e. unit)
		cc = gameObject.GetComponent<CharacterController>();

		// Initialize the destination to the current position, so units don't move when we 
		// press start.
		destination = transform.position;

		// Store the default color.
		defautColor = rend.material.color;
		// Set the initial color.

		// Give Hank a random orientation on spawn
		transform.eulerAngles = new Vector3(0, Random.Range(0,360), 0);
	}

	// Update is called once per frame
	void Update()
	{
		timeLeft -= Time.deltaTime;
		if(timeLeft <= 0)
		{
			SceneManager.LoadScene("EndGame");
		}
		if (destination != null && Vector3.Distance(destination, transform.position) > 0.5f) 
		{
			// If we have a destination, rotate and move towards it.
			destination.y = transform.position.y;
			Vector3 vecToDest = (destination - transform.position).normalized;
			float step = 3 * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, vecToDest, step, 1);
			transform.rotation = Quaternion.LookRotation(newDir);

			cc.Move(transform.forward * 5 * Time.deltaTime);
		}
	}


	// The following functions are called by Unity based on what the mouse is doing with
	// regards to the gameObject this script is attached to.
	private void OnMouseOver()
	{
		gm.timerObject.SetActive(true);
		gm.meterFG.fillAmount = timeLeft / initTime;
		gm.timerObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
	}
	private void OnMouseExit()
	{
		gm.timerObject.SetActive(false);
	}
	private void OnMouseDown()
	{
		selected = !selected;
		if (selected) {
			gm.selectUnit(this);
		} else {
			gm.selectUnit(null);
		}
	}
}
