using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryObject
{
	public void Open();
	public void Close();
}

public class FactoryObject : Component, IFactoryObject
{
	private bool m_isAlive;
	public bool isAlive => m_isAlive;
	
	public virtual void Close()
	{
		m_isAlive = false;
	}
	public virtual void Open()
	{
		m_isAlive = true;
	}
}
