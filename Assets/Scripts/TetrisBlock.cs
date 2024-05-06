using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
	public Vector3 rotationPoint;
	private float previousFallTime;
	private float fallTime;
	private float previousHorizontalTime;
	private float horizontalTime = 0.15f;
	public static int height = 20;
	public static int width = 10;
	private static Transform[,] grid = new Transform[width, height+5];
	public Material startMaterial;
	public Material endMaterial;
	public Material gameOverMaterial;
	private bool gameOver = false;
	private int[] scoringLines = {40, 100, 300, 1200};
	
	//TODO lerp away lines to the sides instead of instant delete?
	//private static Transform[,] lerpCubes = new Transform[width, 4];
	//private static float lerpCubesStartTime;
	
	
    // Start is called before the first frame update
    void Start()
	{
		SetMaterial(startMaterial);
		
		fallTime = Random.Range(0.2f, 0.8f);
		
		int nextRotation = Random.Range(0,4);
		transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3 (0, 1, 0), 90*nextRotation);
		if (!IsValidMove())
			transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3 (0, 1, 0), -90*nextRotation);
    }

    // Update is called once per frame
    void Update()
	{
		TetrisKeyPressHandler();
		
		if ((Time.time - previousFallTime) > fallTime)
		{
			transform.position += new Vector3 (0, 0, -1);
			if (!IsValidMove())
			{
				MoveFinishHandler();
			}
			previousFallTime = Time.time;
		}
		
		//TODO lerp away lines to the sides instead of instant delete?
		//if (lerpCubes.Length != 0)
		//{
		//	journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
		//}
	}
	
	public void NegButtonPressHandler(int i)
	{
		if (!gameOver)
			ShiftRowLeft(i);
	}
	
	public void PosButtonPressHandler(int i)
	{
		if (!gameOver)
			ShiftRowRite(i);
	}
	
	void ShiftRowLeft(int i)
	{	
		Transform temp = grid[0,i];
		for (int j = 1; j < width; j++)
		{
			grid[j-1,i] = grid[j,i];
			if (grid[j,i] != null)
			{
				grid[j,i].transform.position -= new Vector3(1,0,0);
				grid[j,i] = null;
			}
		}
		grid[width-1,i] = temp;
		if (temp != null)
		{
			temp.transform.position += new Vector3(width-1,0,0);
			temp = null;
		}
	}
	
	void ShiftRowRite(int i)
	{
		Transform temp = grid[width-1,i];
		for (int j = width-2; j >= 0; j--)
		{
			grid[j+1,i] = grid[j,i];
			if (grid[j,i] != null)
			{
				grid[j,i].transform.position += new Vector3(1,0,0);
				grid[j,i] = null;
			}
		}
		grid[0,i] = temp;
		if (temp != null)
		{
			temp.transform.position -= new Vector3(width-1,0,0);
			temp = null;
		}
	}
	
	void MoveFinishHandler()
	{
		transform.position -= new Vector3 (0, 0, -1);
		SetMaterial(endMaterial);
		AddToGrid();
		CheckForLines();
		this.enabled = false;
		if (!gameOver)
			FindObjectOfType<SpawnTetromino>().NewTetromino();
		else
			GameOverHandler();
	}
	
	void GameOverHandler()
	{
		SetGameOverMaterialWholeGrid();
	}
	
	void TetrisKeyPressHandler()
	{
		if (Input.GetKey(KeyCode.LeftArrow) && (Time.time - previousHorizontalTime > horizontalTime))
		{
			transform.position += new Vector3 (-1, 0, 0);
			if (!IsValidMove())
				transform.position -= new Vector3 (-1, 0, 0);
			previousHorizontalTime = Time.time;
		}
		else if (Input.GetKey(KeyCode.RightArrow) && (Time.time - previousHorizontalTime > horizontalTime))
		{
			transform.position += new Vector3 (1, 0, 0);
			if (!IsValidMove())
				transform.position -= new Vector3 (1, 0, 0);
			previousHorizontalTime = Time.time;
		}
	
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3 (0, 1, 0), 90);
			if (!IsValidMove())
				transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3 (0, 1, 0), -90);
		}
		
		if ((Time.time - previousFallTime) > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 15 : fallTime))
		{
			transform.position += new Vector3 (0, 0, -1);
			if (!IsValidMove())
			{
				MoveFinishHandler();
			}
			previousFallTime = Time.time;
		}
	}
    
	void SetMaterial(Material mat)
	{
		MeshRenderer mr;
		foreach (Transform children	in transform)
		{
			mr = (children.GetChild(0).GetChild(0).GetComponent<MeshRenderer>());
			mr.material = mat;
		}
	}
	
	void SetGameOverMaterialWholeGrid()
	{
		MeshRenderer mr;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (grid[j,i] != null)
				{
					mr = grid[j,i].GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
					mr.material = gameOverMaterial;
				}
			}
				
		}
		SetMaterial(gameOverMaterial);
	}
    
	void AddToGrid()
	{
		foreach (Transform children	in transform)
		{
			int roundedX = Mathf.RoundToInt(children.transform.position.x);
			int roundedZ = Mathf.RoundToInt(children.transform.position.z);
			
			if (roundedZ < height+5)
			{
				if (grid[roundedX,roundedZ] == null)
					grid[roundedX,roundedZ] = children;
				else
					Destroy(children.gameObject);
			}
			if (roundedZ >= height)
				gameOver = true;
		}
	}
	
	void CheckForLines()
	{
		int continuousLines = -1;
		
		for (int i = height-1; i >= 0; i--)
		{
			if (HasLine(i))
			{
				continuousLines++;
				DeleteLine(i);
				RowsDown(i);
			}
				
		}
		
		if (continuousLines > -1)
		{
			FindObjectOfType<ScoreVarTextHandler>().SetScore(FindObjectOfType<ScoreVarTextHandler>().GetScore()+scoringLines[continuousLines]);
			
			if (FindObjectOfType<ScoreVarTextHandler>().GetScore() > FindObjectOfType<HighVarTextHandler>().GetHigh())
				FindObjectOfType<HighVarTextHandler>().SetHigh(FindObjectOfType<ScoreVarTextHandler>().GetScore());
		}
	}
	
	bool HasLine(int i)
	{
		for (int j = 0; j < width; j++)
		{
			if (grid[j,i] == null)
				return false;
		}
		return true;
	}
	
	void DeleteLine(int i)
	{
		//TODO lerp away lines to the sides instead of instant delete?
		//lerpCubesStartTime = Time.time;
		for (int j = 0; j < width; j++)
		{
			if (grid[j,i] != null)
			{
				Destroy(grid[j,i].gameObject);
				grid[j,i] = null;
			}
		}
	}
	
	public void DeleteAllLines()
	{
		for (int i = 0; i < height+5; i++)
		{
			DeleteLine(i);
		}
	}
	
	void RowsDown(int i)
	{
		for (int y = i; y < height; y++)
		{
			for (int j = 0; j < width; j++)
			{
				if (grid[j,y] != null)
				{
					grid[j,y-1] = grid[j,y];
					grid[j,y] = null;
					grid[j, y-1].transform.position -= new Vector3(0,0,1);
				}
			}
		}
	}
    
	bool IsValidMove()
	{
		foreach (Transform children	in transform)
		{
			
			int roundedX = Mathf.RoundToInt(children.transform.position.x);
			int roundedZ = Mathf.RoundToInt(children.transform.position.z);
			
			if (roundedX < 0 || roundedX >= width || roundedZ < 0)
			{
				return false;
			}
			
			if (roundedZ < height)
				if (grid[roundedX, roundedZ] != null)
					return false;
		}
			
		return true;
	}
	
	public bool GetGameOver ()
	{
		return gameOver;
	}
}
