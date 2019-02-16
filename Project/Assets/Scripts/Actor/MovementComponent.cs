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
        CGameEventManager.GetInstance().AddEventHandler<OnGroundParam>(enGameEvent.ActorOnGroundEvent, OnActorOnGround);
        CGameEventManager.GetInstance().AddEventHandler<int>(enGameEvent.ActorLeaveGroundEvent, OnActorLeaveGround);
    }

    private void RemoveEventListener()
    {
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.LeftArrowEvent, OnLeftArrow);
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.RightArrowEvent, OnRightArrow);
        CGameEventManager.GetInstance().RmvEventHandler<bool>(enGameEvent.SpaceEvent, OnSpace);
        CGameEventManager.GetInstance().RmvEventHandler<OnGroundParam>(enGameEvent.ActorOnGroundEvent, OnActorOnGround);
        CGameEventManager.GetInstance().RmvEventHandler<int>(enGameEvent.ActorLeaveGroundEvent, OnActorLeaveGround);
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
            //actor.linkerComponent.playerObj.transform.parent = null;

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

        MoveEventParam param;
        if (realMoveSpeed != 0 && isMoving == false)
        {
            //之前没有移动，现在开始移动了
            isMoving = true;

            param.isMove = isMoving;
            param.isRight = realMoveSpeed > 0;
            CGameEventManager.GetInstance().SendEvent<MoveEventParam>(enGameEvent.MoveEvent, ref param);
        }
        else if (realMoveSpeed == 0 && isMoving)
        {
            isMoving = false;

            param.isMove = isMoving;
            param.isRight = false;
            CGameEventManager.GetInstance().SendEvent<MoveEventParam>(enGameEvent.MoveEvent, ref param);
        }
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    private void OnActorOnGround(ref OnGroundParam parm)
    {
        if (parm.actorId == actor.ObjId && parm.ground != null)
            isOnGround = true;

        //角色接触地板的时候要检测是否站到了可以移动的板子上，
        //如果站到了可以移动的板子上，需要把父对象设置为这个板子
        if(actor.linkerComponent.playerObj.gameObject.transform.parent != parm.ground.transform)
        {
            actor.linkerComponent.playerObj.gameObject.transform.parent = parm.ground.transform;
        }
    }

    private void OnActorLeaveGround(ref int param)
    {
        if (param == actor.ObjId)
            actor.linkerComponent.playerObj.gameObject.transform.parent = null;
    }

    public void SetActorPos(Vector3 destPos)
    {
        if (actor == null)
            return;
        actor.linkerComponent.playerObj.transform.position = destPos;
    }

}
