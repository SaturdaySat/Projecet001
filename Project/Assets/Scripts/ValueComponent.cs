using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueComponent : BaseComponent
{
    const float moveSpeedTemp = 25.0f;
    const float jumpPowerTemp = 145.0f;

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
        AddEventListener();
    }

    public void Prepare()
    {
    }

    public void UnInit()
    {
        hostActor = null;
        RemoveEventListener();
    }

    public void Update(float deltaTime)
    {
    }

    private void AddEventListener()
    {
   
    }

    private void RemoveEventListener()
    {
 
    }


}
