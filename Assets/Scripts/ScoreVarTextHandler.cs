using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVarTextHandler : MonoBehaviour
{
	Text text;
	public static int scoreVar;
	
	// Start is called before the first frame update
	void Start()
	{
		text = GetComponent<Text>();
		scoreVar = 0;
	}

	// Update is called once per frame
	void Update()
	{
		text.text = "" + scoreVar;
	}
	
	public int GetScore ()
	{
		return scoreVar;
	}
	
	public void SetScore (int i)
	{
		scoreVar=i;
	}
}
