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
        EventManager.GetInstance().AddEventListener(EventName.MoveEvent, OnMoveEvent);
    }

    private void RemoveEventListener()
    {
        EventManager.GetInstance().RmvEventListener(EventName.MoveEvent, OnMoveEvent);
    }

    private void OnMoveEvent(EventParam param)
    {
        if (animator == null)
            return;
        if (param.GetType() != typeof(MoveParam))
            return;
        bool isRun = ((MoveParam)param).isMove;
        bool isRight = ((MoveParam)param).isRight;
        animator.SetBool("isRun",isRun);
        actor.GetComponent<SpriteRenderer>().flipX = !isRight;
    }
}
