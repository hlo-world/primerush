using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
	
	public GameObject[] Tetrominoes;
	
    // Start is called before the first frame update
    void Start()
    {
	    //NewTetromino();
    }

	public void NewTetromino()
	{
		
		int nextTetromino = Random.Range(0,Tetrominoes.Length);
		if (nextTetromino == 0)
			Instantiate(Tetrominoes[nextTetromino], V3AddRandomX(transform.position,10+1-4), Quaternion.identity);
		else if (nextTetromino == 3)
			Instantiate(Tetrominoes[nextTetromino], V3AddRandomX(transform.position,10+1-2), Quaternion.identity);
		else
			Instantiate(Tetrominoes[nextTetromino], V3AddRandomX(transform.position,10+1-3), Quaternion.identity);
	}
	
	Vector3 V3AddRandomX (Vector3 original, int max)
	{
		Vector3 newV3 = original;
		newV3.x += Random.Range(0,max);
		return newV3;
	}
}
