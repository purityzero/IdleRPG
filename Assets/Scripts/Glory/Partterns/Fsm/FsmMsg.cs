using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmMsg
{
    protected int m_msgType;
    public int getMsgType { get { return m_msgType; } }

    public FsmMsg( int _msgType )
    {
        m_msgType = _msgType;
    }
}

public static class FsmMsgDefine
{
    static public FsmMsg animationEnd = new FsmMsg(1);
}
