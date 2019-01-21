using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : BaseComponent{

    public Actor actor;
    private Rigidbody2D rigid2D;
    private BoxCollider2D collier2D;

    //指的是 是否有向右或向左的操作命令，不一定是移动方向
    private bool isRight = false;
    private bool isLeft = false;
    private bool isOnGround = false;

    private bool isMoving = false;

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
        float realMoveSpeed = 0;
        realMoveSpeed = isRight ? realMoveSpeed + actor.valueComponent.MoveSpeed : realMoveSpeed;
        realMoveSpeed = isLeft ? realMoveSpeed - actor.valueComponent.MoveSpeed : realMoveSpeed;

        Vector3 posNow = actor.linkerComponent.playerObj.transform.position;
        posNow.x += realMoveSpeed * deltaTime;
        actor.linkerComponent.playerObj.transform.position = posNow;

        if (realMoveSpeed != 0 && isMoving == false)
        {
            //之前没有移动，现在开始移动了
            isMoving = true;
            EventManager.GetInstance().SendEvent(EventName.MoveEvent, new MoveParam(isMoving, realMoveSpeed > 0));
        }
        else if (realMoveSpeed == 0 && isMoving)
        {
            isMoving = false;
            EventManager.GetInstance().SendEvent(EventName.MoveEvent, new MoveParam(isMoving, false));
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
