using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {

    public void InitManager()
    {
        AddEventListener();
    }

    public void UnInitManager()
    {
        RemoveEventListener();
    }
    private void AddEventListener()
    {
        CGameEventManager.GetInstance().AddEventHandler<int>(enGameEvent.ActorEnterDeadZoneEvent, OnActorEnterDeadZoneEvent);
        CGameEventManager.GetInstance().AddEventHandler<int>(enGameEvent.AcotrDeadEvent, OnActorDeadEvent);
    }

    private void RemoveEventListener()
    {
        CGameEventManager.GetInstance().RmvEventHandler<int>(enGameEvent.ActorEnterDeadZoneEvent, OnActorEnterDeadZoneEvent);
        CGameEventManager.GetInstance().RmvEventHandler<int>(enGameEvent.AcotrDeadEvent, OnActorDeadEvent);
    }

    private void OnActorEnterDeadZoneEvent(ref int objID)
    {
        ActorManager.GetInstance().KillActor(objID);
    }

    private void OnActorDeadEvent(ref int objId)
    {
        if (GameManager.Instance.hostActor.ObjId == objId)
        {
            GameManager.Instance.curLevel.SpawnHostActor();
        }
    }

}
