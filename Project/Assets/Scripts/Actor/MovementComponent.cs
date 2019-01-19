using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : BaseComponent{

    public Actor actor;
    private Rigidbody2D rigid2D;
    private BoxCollider2D collier2D;

    private bool isRight = false;
    private bool isLeft = false;

    private bool isOnGround = false;

    public void Init(Actor actor, string actorPath)
    {
        this.actor = actor;
        AddEventListener();
    }

    public void Prepare()
    {
        rigid2D = actor.linkerComponent.playerObj.GetComponent<Rigidbody2D>();
        collier2D = actor.linkerComponent.playerObj.GetComponent<BoxCollider2D>();
    }

    public void UnInit()
    {
        this.actor = null;
        RemoveEventListener();
    }

    public void Update(float deltaTime)
    {
        if (actor == null || actor.linkerComponent == null || actor.linkerComponent.playerObj == null)
        {
            return;
        }
        //移动
        Move(deltaTime);
    }

    private void AddEventListener()
    {
        CGameEventManager.GetInstance().AddEventHandler<bool>(enGameEvent.LeftArrowEvent, OnLeftArrow);
        CGameEventManager.GetInstance().AddEventHandler<bool>(enGameEvent.RightArrowEvent, OnRightArrow);
        CGameEventManager.GetInstance().AddEventHandler<bool>(enGameEvent.SpaceEvent, OnSpace);
        CGameEventManager.GetInstance().AddEventHandler<int>(enGameEvent.ActorOnGroundEvent, OnActorOnGround);
    }

    private void RemoveEventListener()
    {
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.LeftArrowEvent, OnLeftArrow);
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.RightArrowEvent, OnRightArrow);
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.SpaceEvent, OnSpace);
        CGameEventManager.GetInstance().RmvEventHandler<int>(enGameEvent.ActorOnGroundEvent, OnActorOnGround);
    }

    private void OnLeftArrow(ref bool boolParam)
    {
        isLeft = boolParam;
    }

    private void OnRightArrow(ref bool boolParam)
    {
        isRight = boolParam;
    }

    private void OnSpace(ref bool parm)
    {
        if (actor.ObjId == GameManager.Instance.hostActor.ObjId)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (actor != null)
        {
            if(rigid2D == null)
            {
                return;
            }

            if (isOnGround == false)
            {
                return;
            }

            rigid2D.AddForce(Vector2.up * actor.valueComponent.JumpPower, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void Move(float deltaTime)
    {
        Vector3 posNow = actor.linkerComponent.playerObj.transform.position;
        if (isRight)
        {
            posNow.x += actor.valueComponent.MoveSpeed * deltaTime;
            actor.linkerComponent.playerObj.transform.position = posNow;
        }

        if (isLeft)
        {
            posNow.x -= actor.valueComponent.MoveSpeed * deltaTime;
            actor.linkerComponent.playerObj.transform.position = posNow;
        }
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    private void OnActorOnGround(ref int parm)
    {
        if(parm == actor.ObjId)
            isOnGround = true;
    }


}
