using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : BaseComponent
{
    Actor actor;
    Animator animator;

    public void Init(Actor actor, string actorPath)
    {
        this.actor = actor;
        animator = this.actor.GetComponent<Animator>();
        AddEventListener();
    }

    public void Prepare()
    {

    }

    public void UnInit()
    {
        this.actor = null;
        this.animator = null;
        RemoveEventListener();
    }

    public void Update(float deltaTime)
    {

    }

    private void AddEventListener()
    {
        CGameEventManager.GetInstance().AddEventHandler<MoveEventParam>(enGameEvent.MoveEvent, OnMoveEvent);
    }

    private void RemoveEventListener()
    {
        CGameEventManager.GetInstance().RmvEventHandler<MoveEventParam>(enGameEvent.MoveEvent, OnMoveEvent);
    }

    private void OnMoveEvent(ref MoveEventParam param)
    {
        if (animator == null)
            return;
        bool isRun = param.isMove;
        bool isRight = param.isRight;
        animator.SetBool("isRun",isRun);
        actor.GetComponent<SpriteRenderer>().flipX = !isRight;
    }
}
