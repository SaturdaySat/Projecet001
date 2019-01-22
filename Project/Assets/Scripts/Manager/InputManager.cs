using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    Actor hostActor;
    private bool isEnterDoorArea;


    Actor HostActor
    {
        get
        {
            if (hostActor == null)
                hostActor = GameManager.Instance.hostActor;
            return hostActor;
        }
    }

    // Use this for initialization
    void Start()
    {
        isEnterDoorArea = false;
        CGameEventManager.GetInstance().AddEventHandler<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, OnEnterDoorAreaEvent);
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
        if (Input.GetKeyDown(KeyCode.DownArrow) && isEnterDoorArea)
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
            isEnterDoorArea = param.isEnter;
        }
    }
}