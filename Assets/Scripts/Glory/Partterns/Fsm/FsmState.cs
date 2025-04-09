using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FsmState<T> where T : System.Enum
{
    protected T m_stateType;

    public T getStateType { get { return m_stateType; } }  

    public FsmState(T _stateType)
    {
        m_stateType = _stateType;
    }

	public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void LateUpdate() {}
    public virtual void FixedUpdate() {}
    public virtual void End(){}
}
