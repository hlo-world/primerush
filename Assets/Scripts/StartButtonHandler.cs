using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
	public void Open()
	{
		if (FindObjectOfType<TetrisBlock>() == null || FindObjectOfType<TetrisBlock>().GetGameOver())
			FindObjectOfType<GameUIHandler>().StartNewGame();
	}
}
