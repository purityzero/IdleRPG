using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPooling<T> where T : Component
{
    private readonly int m_MaxCount;
    private List<T> m_ActiveList = new List<T>();
    private List<T> m_HideList = new List<T>();

    private string m_Path;
    private Transform m_Parent;

    public MemoryPooling(int _maxCount, string _path, Transform parent)
    {
        this.m_MaxCount = _maxCount;
        this.m_Path = _path;
        this.m_Parent = parent;
    }

    public T Pop()
    {
        T obj = null;

        if (m_HideList.Count > 0)
        {
            int lastIndex = m_HideList.Count - 1;
            obj = m_HideList[lastIndex];
            m_HideList.RemoveAt(lastIndex);
        }
        else
        {
            obj = ResUtil.Create<T>(m_Path, m_Parent, true);
        }

        m_ActiveList.Add(obj);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public bool Push(T obj)
    {
        bool isActive = m_ActiveList.Remove(obj);
        if (isActive)
        {
            obj.gameObject.SetActive(false);
            m_HideList.Add(obj);
        }
        return isActive;
    }

    public void Clear()
    {
        foreach (var obj in m_ActiveList)
        {
            GameObject.Destroy(obj.gameObject);
        }
        foreach (var obj in m_HideList)
        {
            GameObject.Destroy(obj.gameObject);
        }

        m_ActiveList.Clear();
        m_HideList.Clear();
    }

    public virtual void UpdateLogic()
    {
		// 
    }
}


