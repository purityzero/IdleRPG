using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] GameObject m_PlayerObj;
	[SerializeField] Camera m_Camera;
    void Update()
    {
		if(!MovementBoundsGizmo.instance.IsOutOfBounds(m_PlayerObj.transform.position + UIJoystick.instance.inputVector *0.1f))
		{
       		 m_PlayerObj.transform.position = m_PlayerObj.transform.position + UIJoystick.instance.inputVector * 0.1f;
			 m_Camera.transform.position =  new Vector3(m_PlayerObj.transform.position.x, 0, -10);
		}
    }
}
