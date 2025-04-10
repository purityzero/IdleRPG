using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] GameObject m_PlayerObj;
	[SerializeField] SpriteRenderer m_SprPlayer;
	[SerializeField] Camera m_Camera;
	[SerializeField] GameObject[] m_ListAttackObj;
	[SerializeField] GizmoColliderBinder2D[] m_ListGizmoColliderBinder2D;

	private bool m_IsAttack = false;
	private float m_AttackTimer	= 0;

	private void Start()
	{
		for (int i=0, iMax = m_ListGizmoColliderBinder2D.Length; i<iMax; i++)
		{
			m_ListGizmoColliderBinder2D[i].OnTrigger += OnAttack;
		}
	}

	public 
    void Update()
    {

		if(m_IsAttack == true)
		{
			m_AttackTimer += Time.deltaTime;
			if(m_AttackTimer > 0.2f)
			{
				m_IsAttack = false;
				AttackShow(true);
			}
		}
		else
		{
			AttackShow(false);
		}

		if(UIJoystick.instance.inputVector == Vector3.zero)
			return;

		// 애니메이션이 끝나면 돌아보게 할 것.
		if(m_IsAttack == false)
		{
			if(UIJoystick.instance.inputVector.x > 0)
				m_SprPlayer.flipX = true;
			else if(UIJoystick.instance.inputVector.x < 0)
			m_SprPlayer.flipX = false;
		}

		if(MovementBoundsGizmo.instance.IsOutOfBounds(m_PlayerObj.transform.position + UIJoystick.instance.inputVector *0.1f) == false)
		{
       		 m_PlayerObj.transform.position = m_PlayerObj.transform.position + UIJoystick.instance.inputVector * 0.1f;
			 m_Camera.transform.position =  new Vector3(m_PlayerObj.transform.position.x, 0, -10);
		}
    }

	public void OnAttackBtn()
	{
		if(m_IsAttack == true)
			return;

		m_AttackTimer = 0;
		m_IsAttack = true;
		AttackShow(true);
	}

	public void AttackShow(bool isShow)
	{
		if(isShow == true && m_SprPlayer.flipX  == true)
		{
			m_ListAttackObj[0].SetActive(isShow);
		}
		else
		{
			m_ListAttackObj[1].SetActive(isShow);
		}

		if(isShow == false)
		{
			m_ListAttackObj[0].SetActive(false);
			m_ListAttackObj[1].SetActive(false);
		}
	}

	public void OnAttack(Collider2D _collider)
	{
		if(_collider.gameObject.CompareTag("Enemy") == true)
		{
			Debug.Log("OnAttack");
			//Destroy(_collider.gameObject);
		}
	}
}
