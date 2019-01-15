using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : BaseComponent{

    public Actor hostActor;
    private Rigidbody2D rigid2D;
    private BoxCollider2D collier2D;

    private bool isRight = false;
    private bool isLeft = false;

    private bool isOnGround = false;

    public void Init(Actor actor, string actorPath)
    {
        hostActor = actor;
        AddEventListener();
    }

    public void Prepare()
    {
        rigid2D = hostActor.linkerComponent.playerObj.GetComponent<Rigidbody2D>();
        collier2D = hostActor.linkerComponent.playerObj.GetComponent<BoxCollider2D>();
    }

    public void UnInit()
    {
        RemoveEventListener();
    }

    public void Update(float deltaTime)
    {
        if (hostActor == null || hostActor.linkerComponent == null || hostActor.linkerComponent.playerObj == null)
        {
            return;
        }
        //移动
        Move(deltaTime);
    }

    private void AddEventListener()
    {
        EventManager.GetInstance().AddEventListener(EventName.LeftArrow, OnLeftArrow);
        EventManager.GetInstance().AddEventListener(EventName.RightArrow, OnRightArrow);
        EventManager.GetInstance().AddEventListener(EventName.Space, OnSpace);
        EventManager.GetInstance().AddEventListener(EventName.ActorOnGround, OnActorOnGround);
    }

    private void RemoveEventListener()
    {
        EventManager.GetInstance().RmvEventListener(EventName.LeftArrow, OnLeftArrow);
        EventManager.GetInstance().RmvEventListener(EventName.RightArrow, OnRightArrow);
        EventManager.GetInstance().RmvEventListener(EventName.Space, OnSpace);
        EventManager.GetInstance().RmvEventListener(EventName.ActorOnGround, OnActorOnGround);
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
        Jump();
    }

    private void Jump()
    {
        if (hostActor != null)
        {
            if(rigid2D == null)
            {
                return;
            }

            if (isOnGround == false)
            {
                return;
            }

            rigid2D.AddForce(Vector2.up * hostActor.valueComponent.JumpPower, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void Move(float deltaTime)
    {
        Vector3 posNow = hostActor.linkerComponent.playerObj.transform.position;
        if (isRight)
        {
            posNow.x += hostActor.valueComponent.MoveSpeed * deltaTime;
            hostActor.linkerComponent.playerObj.transform.position = posNow;
        }

        if (isLeft)
        {
            posNow.x -= hostActor.valueComponent.MoveSpeed * deltaTime;
            hostActor.linkerComponent.playerObj.transform.position = posNow;
        }
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    private void OnActorOnGround(EventParam parm)
    {
        isOnGround = true;
    }


}
