using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    Actor hostActor;
    private bool isEnterDoorArea;

    private int enterDoorCount;



    public Actor HostActor
    {
        get
        {
            if (hostActor == null)
                hostActor = GameManager.Instance.hostActor;
            return hostActor;
        }
        set
        {
            hostActor = value;
        }
    }

    bool IsEnterDoorArea
    {
        get {
            return enterDoorCount > 0;
        }
    }

    // Use this for initialization
    void Start()
    {
        enterDoorCount = 0;
        isEnterDoorArea = false;
        CGameEventManager.GetInstance().AddEventHandler<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, OnEnterDoorAreaEvent);
        CGameEventManager.GetInstance().AddEventHandler<Actor>(enGameEvent.ActorSpawnEvent, OnActorSpawnEvent);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.SpaceEvent, ref parm);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.LeftArrowEvent, ref parm);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.RightArrowEvent, ref parm);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            bool parm = false;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.LeftArrowEvent, ref parm);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            bool parm = false;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.RightArrowEvent, ref parm);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsEnterDoorArea)
        {
            int actorID = GameManager.Instance.hostActor.ObjId;
            CGameEventManager.GetInstance().SendEvent<int>(enGameEvent.EnterDoorActionEvent, ref actorID);
        }
    }

    private void OnEnterDoorAreaEvent(ref EnterDoorAreaParam param)
    {
        if (HostActor == null)
            return;
        if (param.actorId == HostActor.ObjId)
        {
            enterDoorCount += param.isEnter ? 1 : -1;
        }
    }

    private void OnActorSpawnEvent(ref Actor actor)
    {
        if (actor != null)
        {
            HostActor = actor;        
        }
    }

}