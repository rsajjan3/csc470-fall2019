using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
	public CellScript[,] grid;
	private GameObject[,] cubeObj;

	public GameObject cellPrefab;
	public GameObject cubePrefab;

	private int time = 0;
	private bool simulate;

	private int gridWidth;
	private int gridHeight;

	private float cellDimension;
	private float cellSpacing;

	private float generationRate;
	private float generationTimer;

	// Start is called before the first frame update
	void Start()
	{
		initValues();

		this.grid = new CellScript[this.gridHeight, this.gridWidth];
		this.cubeObj = new GameObject[this.gridHeight, this.gridWidth];

		//Using nested for loops, instantiate cubes with cell scripts in a way such that
		//	each cell will be places in a top left oriented coodinate system.
		//	I.e. the top left cell will have the x, y coordinates of (0,0), and the bottom right will
		//	have the coodinate (gridHeight-1, gridWidth-1)
		for (int row = 0; row < this.gridHeight; row++) 
		{
			for (int col = 0; col < this.gridWidth; col++) 
			{
				//Create a cube, position/scale it, add the CellScript to it.
				Vector3 pos = new Vector3(row * (this.cellDimension + this.cellSpacing), 0, col * (this.cellDimension + this.cellSpacing));

				GameObject cellObj = Instantiate(this.cellPrefab, pos, Quaternion.identity);
				this.cubeObj[row, col] = Instantiate(this.cubePrefab, pos + new Vector3(0, 3.3f, 0), Quaternion.identity);

				CellScript cs = cellObj.AddComponent<CellScript>(); 
				cs.value = Random.Range(0, 2); //0 or 1
				cs.updateColor();

				cellObj.transform.position = pos;
				cellObj.transform.localScale = new Vector3(cellDimension, cellDimension, cellDimension);
				//Finally add the cell to it's place in the two dimensional array
				this.grid[row, col] = cs;
			}
		}
		//Initialize the timer
		this.generationTimer = this.generationRate;
	}

	private void Update()
	{
		this.generationTimer -= Time.deltaTime;
		if (this.generationTimer < 0 && this.simulate) {
			//generate next state
			generate();

			//reset the timer
			this.generationTimer = this.generationRate;
		}
	}

	void initValues()
	{
		time = 0;
        simulate = false;
		
		gridWidth = 10;
		gridHeight = 10;
		
		cellDimension = 3.3f;
		cellSpacing = 0.2f;
		
		generationRate = 1f;
	}

	void generate()
	{
		/*
			1.) Any live cell with fewer than two live neighbours dies, as if by underpopulation.
			2.) Any live cell with two or three live neighbours lives on to the next generation.
			3.) Any live cell with more than three live neighbours dies, as if by overpopulation.
			4.) Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
		*/
		/*
		   -1: Revive
			0: Dead
			1: Alive
			2: Kill
		*/

		this.time++;
		for(int row = 0; row < this.gridHeight; row++)
        {
            for(int col = 0; col < this.gridWidth; col++)
            {
                int alive = LiveNeighborCount(row, col);
                if(this.grid[row, col].value == 1 && (alive < 2 || alive > 3)) //Breaks rule 1 and rule 3
                {
                	this.grid[row, col].value = 2; //Signal that cell needs to die
                }
				else if(grid[row, col].value == 1 && (alive == 2 || alive == 3) ) //Satisfies Rule 2
				{
					this.cubeObj[row, col].GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
				}
                else if(grid[row, col].value == 0 && alive == 3) //Satisfies Rule 4
                {
                    this.grid[row, col].value = -1;
					this.cubeObj[row, col].GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                }
                //Rule 2 does not require any special case, because its state doesn't change on the board
            }
        }
        ConvertBoard();
    }

	int LiveNeighborCount(int startRow, int startCol)
	{
		int alive = 0;
		int rowLength = this.gridHeight;
		int colLength = this.gridWidth;

		for(int row = Mathf.Max(0, startRow - 1); row < Mathf.Min(rowLength, startRow + 2); row++)
		{
			for (int col = Mathf.Max(0, startCol - 1); col < Mathf.Min(colLength, startCol + 2); col++)
			{
				if (this.grid[row, col].value >= 1) // 2 counts as alive
				{
					alive++;
				}
			}
		}

		alive = alive - this.grid[startRow, startCol].value; // Don't count self
		return alive;
	}

    public void ConvertBoard()
    {
        for (int row = 0; row < this.gridHeight; row++)
        {
            for (int col = 0; col < this.gridWidth; col++)
            {
                if (this.grid[row, col].value == -1) 
				{
					this.grid[row, col].value = 1;
				}
                else if (this.grid[row, col].value == 2) this.grid[row, col].value = 0;
            }
        }
    }

	//This function is called by the UI toggle's event system (look at the ToggleSimulateButton
	//child of the Canvas)
	public void toggleSimulate(bool value)
	{
		this.simulate = value;
	}
}