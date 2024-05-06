using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighVarTextHandler : MonoBehaviour
{
	Text text;
	public static int highVar;
	
	// Start is called before the first frame update
	void Start()
	{
		text = GetComponent<Text>();
		highVar = 0;
	}

	// Update is called once per frame
	void Update()
	{
		text.text = "" + highVar;
	}
	
	public int GetHigh ()
	{
		return highVar;
	}
	
	public void SetHigh (int i)
	{
		highVar=i;
	}
}
