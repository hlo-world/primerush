using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	private float moveSpeed;
	public Animator anim;
	public string Axis_Hori;
	public string Axis_Vert;
	public string Axis_Actn;
	
    // Start is called before the first frame update
    void Start()
    {
	    moveSpeed = 8f;
	    anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
	{
		float moveHorizontal = Input.GetAxis(Axis_Hori);
		float moveVertical = Input.GetAxis(Axis_Vert);
		
		if (moveHorizontal != 0 || moveVertical != 0)
			anim.SetBool("isRunning", true);
		else
			anim.SetBool("isRunning", false);
			
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(movement);
			transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);
		}
    }
}
