using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueComponent {
    const float moveSpeedTemp = 5.0f;

    public Actor hostActor;
    private float moveSpeed;

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

    public void Init(Actor actor)
    {
        hostActor = actor;
        moveSpeed = moveSpeedTemp;
    }





}
