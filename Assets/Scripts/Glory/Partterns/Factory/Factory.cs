using System;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
	public T Create<T,U>(U Type) where T : FactoryObject where U : struct, Enum;

}

public interface IFactoryMemoryPoolling
{
	public FactoryObject Pop();

}

public class Factory : IFactory
{
	
	public virtual T Create<T,U>(U Type)	where T : FactoryObject where U : struct, Enum
	{
		T obj = null;

		return obj;
	}
}

public class MemoryPoolFactory<T1, TEnum1> : Factory	where T1 : FactoryObject
													where TEnum1 : struct, Enum
{
	//private MemoryPooling<FactoryObject> m_MemoryPool = null;
	private Dictionary<TEnum1, MemoryPooling<T1>> m_MemoryPoolDictionary = new Dictionary<TEnum1, MemoryPooling<T1>>();
    public MemoryPoolFactory(int _maxCount, string _path, Transform _parent)
    {
        foreach (TEnum1 enumValue in Enum.GetValues(typeof(TEnum1)))
        {
            m_MemoryPoolDictionary.Add(enumValue, new MemoryPooling<T1>(_maxCount, _path, _parent));
        }

    }

	public override T Create<T, U>(U type)
	{
		if (!Enum.TryParse(type.ToString(), out TEnum1 enumKey))
		{
			Debug.LogError($"Invalid Enum Value: {type}");
			return null;
		}

		if (!m_MemoryPoolDictionary.ContainsKey(enumKey))
		{
			Debug.LogError($"Invalid Enum Value: {type}");
			return null;
		}

		return m_MemoryPoolDictionary[enumKey].Pop() as T;
	}

	public virtual void UpdateLogic()
    {
        foreach (var pool in m_MemoryPoolDictionary.Values)
        {
            pool.UpdateLogic();
        }
    }

}