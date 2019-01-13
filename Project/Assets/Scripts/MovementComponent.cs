using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent  {

    public Actor hostActor;

    private bool isRight = false;
    private bool isLeft = false;

    public void Init(Actor actor)
    {
        hostActor = actor;
        AddEventListener();
    }

    public void UnInit()
    {
        RemoveEventListener();
    }

    public void Update(float deltaTime)
    {
        if (hostActor != null && hostActor.linkerComponent != null && hostActor.linkerComponent.playerObj)
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
    }

    private void AddEventListener()
    {
        EventManager.GetInstance().AddEventListener(EventName.LeftArrow, OnLeftArrow);
        EventManager.GetInstance().AddEventListener(EventName.RightArrow, OnRightArrow);
    }

    private void RemoveEventListener()
    {
        EventManager.GetInstance().RmvEventListener(EventName.LeftArrow, OnLeftArrow);
        EventManager.GetInstance().RmvEventListener(EventName.RightArrow, OnRightArrow);
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



}
