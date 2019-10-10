using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
	public int row { get; set; }
	
	public int col { get; set; }
	
	public int value { get; set; }
	
	private Renderer rend;

	// Start is called before the first frame update
	void Start()
    {
		this.row = -1;
		this.col = -1;
	}

    // Update is called once per frame
    void Update()
    {
		updateColor();
	}

	public void updateColor()
	{
		if (this.rend == null) { this.rend = gameObject.GetComponentInChildren<Renderer>(); }

		//if (this.isAlive) { this.rend.material.color = Color.red; }
		if(this.value == 1) { this.rend.material.color = Color.red; }
		else { this.rend.material.color = Color.blue; }
	}

	private void OnMouseDown()
	{
		if(this.value == 1) this.value = 0;
		else this.value = 1;
	}
}
