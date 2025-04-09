using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 유한상태기계. 
 */
[System.Serializable]
public class FsmClass<T> where T : System.Enum
{
    public string strState;
    protected Dictionary<T, FsmState<T>> m_stateList = new Dictionary<T, FsmState<T>>();
    protected FsmState<T> m_state;    
    protected bool m_isStateChanging = false;//FsmState::Enter(), FsmState::End()에서 FsmClass::SetState 함수 호출을 막기위한 변수.

    public FsmState<T> getState { get { return m_state; } }
    public bool IsState(T _stateType)
    {
        if (null == m_state)
            return false;

        return m_state.getStateType.CompareTo(_stateType) == 0;
    }

    public virtual void Init()
    {
        
    }

    public virtual void Clear()
    {
        m_stateList.Clear();
        m_state = null;
    }

    /**
     * AddFsm() 
     * 사용할 상태를 등록한다.
     */
    public virtual void AddFsm(FsmState<T> _state)
    {
        if( null == _state )
        {
            Debug.LogErrorFormat("SFsmState == null : {0} ", _state.getStateType);
            return;
        }

        if (true == m_stateList.ContainsKey(_state.getStateType))
        {
            Debug.LogErrorFormat("have state : {0} ", _state.getStateType);
            return;
        }

        m_stateList.Add(_state.getStateType, _state);
    }

    /**
     * SetState()
     * 상태를 변경한다.
     */
    public virtual void SetState(T _stateType)
    {
        if (false == m_stateList.ContainsKey(_stateType))
        {
            Debug.LogError("no have state : " + _stateType);
            return;
        }

        if (m_isStateChanging == true)
        {
            Debug.LogErrorFormat("state changing : {0}", _stateType);
            return;
        }

        FsmState<T> _nextState = m_stateList[_stateType];
        if (_nextState == m_state)
        {
            Debug.LogWarningFormat("same state : {0}", _stateType);
        }

       
        m_isStateChanging = true;

        if (null != m_state)
        {           
            m_state.End();
        }

        m_state = _nextState;
		m_state.Enter();
        m_isStateChanging = false;
    }  

    #region - Update
    public virtual void Update()
    {
        if (null == m_state)
            return;

        m_state.Update();
        strState = m_state.getStateType.ToString();
    }

    public virtual void LateUpdate()
    {
        if (null == m_state)
            return;
        m_state.LateUpdate();
    }    

    public virtual void FixedUpdate()
    {
        if (null == m_state)
            return;            
        m_state.FixedUpdate();
    }
    #endregion
}
