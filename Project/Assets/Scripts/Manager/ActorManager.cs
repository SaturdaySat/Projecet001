using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : Singleton<ActorManager> {

    public int curObjId;
    public Dictionary<int, Actor> actorDict;

    public void InitManager()
    {
        curObjId = 0;
        actorDict = new Dictionary<int, Actor>();
    }

    public Actor CreateActor(string actorPath)
    {
        Actor newActor = new Actor(actorPath);
        newActor.ObjId = curObjId;
        newActor.Init();
        newActor.Prepare();
        actorDict.Add(curObjId, newActor);
        curObjId++;

        return newActor;
    }

    public Actor GetActor(int objID)
    {
        Actor actor = null;
        actorDict.TryGetValue(objID, out actor);
        return actor;
    }

    public void KillActor(int objID)
    {
        Actor actor = GetActor(objID);
        if (actor == null)
            return;
        actorDict.Remove(objID);
        actor.UnInit();

        CGameEventManager.GetInstance().SendEvent<int>(enGameEvent.AcotrDeadEvent, ref objID);
    }
}
