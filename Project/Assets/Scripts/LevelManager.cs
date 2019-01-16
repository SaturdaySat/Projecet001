using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {

    public void InitManager()
    {
        AddEventListener();
    }

    private void AddEventListener()
    {
        EventManager.GetInstance().AddEventListener(EventName.ActorEnterDeadZoneEvent, OnActorEnterDeadZoneEvent);
        EventManager.GetInstance().AddEventListener(EventName.AcotrDeadEvent, OnActorDeadEvent);
    }

    private void RemoveEventListener()
    {
        EventManager.GetInstance().RmvEventListener(EventName.ActorEnterDeadZoneEvent, OnActorEnterDeadZoneEvent);
    }

    private void OnActorEnterDeadZoneEvent(EventParam parm)
    {
        int objId = ((CommonIntParam)parm).intval;  //角色的唯一识别ID
        ActorManager.GetInstance().KillActor(objId);
    }

    private void OnActorDeadEvent(EventParam parm)
    {
        int objId = ((CommonIntParam)parm).intval;
        if (GameManager.Instance.hostActor.ObjId == objId)
        {
            GameManager.Instance.curLevel.SpawnHostActor();
        }
    }

}
