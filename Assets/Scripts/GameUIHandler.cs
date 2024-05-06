using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
	public static int width = 10;
	public static int height = 20;
	public static int border = 2;
	public GameObject ArenaButton;
	bool[,] placeButton = new bool[border,height];
	private static GameObject[,] arenaButtons = new GameObject[border, height];
	
	public void StartNewGame()
	{
		WipeAllButtons();
		SetButtonBoolArray(placeButton);
		
		FindObjectOfType<ScoreVarTextHandler>().SetScore(0);
		FindObjectOfType<SpawnTetromino>().NewTetromino();
		FindObjectOfType<TetrisBlock>().DeleteAllLines();
	}
	
	void SetButtonBoolArray (bool[,] buttonArray)
	{
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < border; j++)
			{
				buttonArray[j,i] = RandomBoolean();
				
				if (i == 0)
					buttonArray[j,i] = true;
				
				
				if (buttonArray[j,i])
				{
					if (j == 0)
						PlaceAButton(j,i);
					else
						PlaceAButtonAndRotate(j,i);
				}
			}
		}
	}
	
	void WipeAllButtons()
	{
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < border; j++)
			{
				if (arenaButtons[j,i] != null)
				{
					Destroy (arenaButtons[j,i]);
					arenaButtons[j,i] = null;
				}
			}
		}
	}
	
	void PlaceAButton(int j, int i)
	{
		arenaButtons[j,i] = Instantiate(ArenaButton, new Vector3 (0f-1+j*(width+1),0f,0f+i), Quaternion.identity);
	}
	
	void PlaceAButtonAndRotate(int j, int i)
	{
		arenaButtons[j,i] = Instantiate(ArenaButton, new Vector3 (0f-1+j*(width+1),0f,0f+i), Quaternion.Euler(0f, 180f, 0f));
	}
	
	bool RandomBoolean()
	{
		return (Random.value > 0.5f);
	}
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
