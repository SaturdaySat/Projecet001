using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPair : MonoBehaviour {

    public Transform DoorATrans;
    public Transform DoorBTrans;

    private Door DoorA;
    private Door DoorB;

    private BoxCollider2D colliderDoorA;
    private BoxCollider2D colliderDoorB;

    private List<int> actorIdListDoorA;
    private List<int> actorIdListDoorB;

	void Start ()
    {
        if (DoorATrans == null || DoorBTrans == null)
            return;
        colliderDoorA = DoorATrans.GetComponent<BoxCollider2D>();
        colliderDoorB = DoorBTrans.GetComponent<BoxCollider2D>();
        DoorA = DoorATrans.GetComponent<Door>();
        DoorB = DoorBTrans.GetComponent<Door>();
        actorIdListDoorA = new List<int>();
        actorIdListDoorB = new List<int>();

        AddEventListener();
	}

    void OnDestroy()
    {
        RemoveEventListener();
    }

    private void AddEventListener()
    {
        CGameEventManager.GetInstance().AddEventHandler<int>(enGameEvent.EnterDoorActionEvent, OnEnterDoor);
    }

    private void RemoveEventListener()
    {
        CGameEventManager.GetInstance().RmvEventHandler<int>(enGameEvent.EnterDoorActionEvent, OnEnterDoor);
    }

    public void ActorEnterDoorArea(Door door, int actorId)
    {
        EnterDoorAreaParam param;
        param.actorId = actorId;
        param.isEnter = true;
        if (door == DoorA)
        {
            if (!actorIdListDoorA.Contains(actorId))
            {
                actorIdListDoorA.Add(actorId);
                CGameEventManager.GetInstance().SendEvent<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, ref param);
            }
        }
        if (door == DoorB)
        {
            if (!actorIdListDoorB.Contains(actorId))
            {
                actorIdListDoorB.Add(actorId);
                CGameEventManager.GetInstance().SendEvent<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, ref param);
            }
        }
    }

    public void ActorLeaveDoorArea(Door door, int actorId)
    {
        EnterDoorAreaParam param;
        param.actorId = actorId;
        param.isEnter = false;
        if (door == DoorA)
        {
            if (actorIdListDoorA.Contains(actorId))
            {
                actorIdListDoorA.Remove(actorId);
                CGameEventManager.GetInstance().SendEvent<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, ref param);
            }
        }
        if (door == DoorB)
        {
            if (actorIdListDoorB.Contains(actorId))
            {
                actorIdListDoorB.Remove(actorId);
                CGameEventManager.GetInstance().SendEvent<EnterDoorAreaParam>(enGameEvent.EnterDoorAreaEvent, ref param);
            }
        }
    }

    private void OnEnterDoor(ref int actorId)
    {
        Actor actor = ActorManager.GetInstance().GetActor(actorId);
        if (actorIdListDoorA.Contains(actorId))
        {
            //Actor在Door A, 需要传送到门B
            if(actor != null)
            {
                actor.movementComponent.SetActorPos(DoorBTrans.position);
            }
        }
        else if (actorIdListDoorB.Contains(actorId))
        {
            //Actor在Door B, 需要传送到门A
            if (actor != null)
            {
                actor.movementComponent.SetActorPos(DoorATrans.position);
            }
        }
    }
	
}
