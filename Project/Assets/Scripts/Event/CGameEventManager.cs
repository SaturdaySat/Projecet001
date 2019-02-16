using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum enGameEvent
{
    None,
    LeftArrowEvent,
    RightArrowEvent,
    EnterDoorAreaEvent,
    EnterDoorActionEvent,
    SpaceEvent,
    ActorOnGroundEvent,
    ActorLeaveGroundEvent,
    ActorEnterDeadZoneEvent,
    AcotrDeadEvent,
    ActorSpawnEvent,
    MoveEvent,
    EnterTriggerArea, 
    LeaveTriggerArea,
    Max,
}


public struct CommonTriggerAreaEventParam
{
    public int actorId;
    public int triggerAreaId;

    public CommonTriggerAreaEventParam(int _actorId, int _triggerAreaId)
    {
        actorId = _actorId;
        triggerAreaId = _triggerAreaId;
    }
}

public struct MoveEventParam
{
    public bool isMove;
    public bool isRight;

    public MoveEventParam(bool _isMove, bool _isRight)
    {
        isMove = _isMove;
        isRight = _isRight;
    }
}


public struct EnterDoorAreaParam
{
    public int actorId;
    public bool isEnter;

    public EnterDoorAreaParam(int _actorId, bool _isEnter)
    {
        actorId = _actorId;
        isEnter = _isEnter;
    }
}

public struct OnGroundParam
{
    public int actorId;
    public GameObject ground;

    public OnGroundParam(int _actorId, GameObject _groundObj)
    {
        actorId = _actorId;
        ground = _groundObj;
    }
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
