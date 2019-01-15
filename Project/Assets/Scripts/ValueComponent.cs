using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueComponent : BaseComponent
{
    const float moveSpeedTemp = 7.0f;
    const float jumpPowerTemp = 15.0f;

    public Actor hostActor;
    private float moveSpeed;
    private float jumpPower;

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }

    public float JumpPower
    {
        get
        {
            return jumpPower;
        }
        set
        {
            jumpPower = value;
        }
    }

    public void Init(Actor actor, string actorPath)
    {
        hostActor = actor;
        moveSpeed = moveSpeedTemp;
        jumpPower = jumpPowerTemp;
    }

    public void Prepare()
    {
    }

    public void UnInit()
    {
    }

    public void Update(float deltaTime)
    {
    }
}
