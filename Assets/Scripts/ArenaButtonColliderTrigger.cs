using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaButtonColliderTrigger : MonoBehaviour
{
	private Light lt;
	private bool m_isAxisInUse;
	private float checkActn1;
	private float checkActn2;
	private float checkActn3;
	private float checkActn4;
	public string Axis_Actn1;
	public string Axis_Actn2;
	public string Axis_Actn3;
	public string Axis_Actn4;
	public string Name_Actn1;
	public string Name_Actn2;
	public string Name_Actn3;
	public string Name_Actn4;
	
	private void OnTriggerEnter(Collider instigator)
	{
		lt = transform.GetChild(1).GetComponent<Light>();
		m_isAxisInUse = false;
    }
    
	private void OnTriggerStay(Collider instigator)
	{
		lt.intensity = 4;
		
		if (instigator.name == Name_Actn1)
			checkActn1 = Input.GetAxis(Axis_Actn1);
		if (instigator.name == Name_Actn2)
			checkActn2 = Input.GetAxis(Axis_Actn2);
		if (instigator.name == Name_Actn3)
			checkActn3 = Input.GetAxis(Axis_Actn3);
		if (instigator.name == Name_Actn4)
			checkActn4 = Input.GetAxis(Axis_Actn4);
		
		if(checkActn1 < 0 || checkActn2 < 0 || checkActn3 < 0 || checkActn4 < 0)
		{
			if(!m_isAxisInUse)
			{
				// Call your event function here.
				int i = (int)transform.parent.position.z;
				FindObjectOfType<TetrisBlock>().NegButtonPressHandler(i);
				m_isAxisInUse = true;
			}
		}
		else if (checkActn1 > 0 || checkActn2 > 0 || checkActn3 > 0 || checkActn4 > 0)
		{
			if(!m_isAxisInUse)
			{
				// Call your event function here.
				int i = (int)transform.parent.position.z;
				FindObjectOfType<TetrisBlock>().PosButtonPressHandler(i);
				m_isAxisInUse = true;
			}
		}
		if(checkActn1 == 0 && checkActn2 == 0 && checkActn3 == 0 && checkActn4 == 0)
			m_isAxisInUse = false;

	}
	
	private void OnTriggerExit(Collider instigator)
	{
		lt.intensity = 0;
	}
}
