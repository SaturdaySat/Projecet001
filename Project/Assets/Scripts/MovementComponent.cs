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
        EventManager.GetInstance().AddEventListener(EventName.LeftArrowEvent, OnLeftArrow);
        EventManager.GetInstance().AddEventListener(EventName.RightArrowEvent, OnRightArrow);
        EventManager.GetInstance().AddEventListener(EventName.SpaceEvent, OnSpace);
        EventManager.GetInstance().AddEventListener(EventName.ActorOnGroundEvent, OnActorOnGround);
    }

    private void RemoveEventListener()
    {
        EventManager.GetInstance().RmvEventListener(EventName.LeftArrowEvent, OnLeftArrow);
        EventManager.GetInstance().RmvEventListener(EventName.RightArrowEvent, OnRightArrow);
        EventManager.GetInstance().RmvEventListener(EventName.SpaceEvent, OnSpace);
        EventManager.GetInstance().RmvEventListener(EventName.ActorOnGroundEvent, OnActorOnGround);
    }

    private void OnLeftArrow(EventParam parm)
    {
        if (parm.GetType() != typeof(CommonBoolParam))
        {
            return;
        }

        CommonBoolParam boolParam = (CommonBoolParam)parm;
        isLeft = boolParam.boolval;
    }

    private void OnRightArrow(EventParam parm)
    {
        if (parm.GetType() != typeof(CommonBoolParam))
        {
            return;
        }

        CommonBoolParam boolParam = (CommonBoolParam)parm;
        isRight = boolParam.boolval;
    }

    private void OnSpace(EventParam parm)
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

    private void OnActorOnGround(EventParam parm)
    {
        if(((CommonIntParam)parm).intval == actor.ObjId);
            isOnGround = true;
    }


}
