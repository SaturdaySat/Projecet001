using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum enGameEvent
{
    None,
    LeftArrowEvent,
    RightArrowEvent,
    SpaceEvent,
    ActorOnGroundEvent,
    ActorEnterDeadZoneEvent,
    AcotrDeadEvent,
    Max,
}

public class CGameEventManager : Singleton<CGameEventManager> {
    private CEventDispatcher m_eventDispatcher = new CEventDispatcher();

    public void InitManager()
    {
    }

    public void AddEventHandler<ParamType>(enGameEvent _event, RefAction<ParamType> _handler)
    {
        m_eventDispatcher.AddEventHandler<ParamType>((int)_event, _handler);
    }

    public void RmvEventHandler<ParamType>(enGameEvent _event, RefAction<ParamType> _handler)
    {
        m_eventDispatcher.RemoveEventHandler<ParamType>((int)_event, _handler);
    }

    public void SendEvent<ParamType>(enGameEvent _event, ref ParamType _param)
    {
        m_eventDispatcher.BroadCastEvent<ParamType>((int)_event, ref _param);
    }

    public void Clear()
    {
        m_eventDispatcher.ClearAllEvents();
    }

}
