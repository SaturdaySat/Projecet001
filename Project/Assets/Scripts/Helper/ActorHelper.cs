using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHelper : MonoBehaviour {

    private float ActorCurSpeed;
    private float ActorCurJumpPower;

    public float ActorSpeed;
    public float ActorJumPower;
    public int ActorObjId;

    public Actor actor;

	// Use this for initialization
	void Start () {
        ActorCurSpeed = actor.valueComponent.MoveSpeed;
        ActorSpeed = ActorCurSpeed;

        ActorCurJumpPower = actor.valueComponent.JumpPower;
        ActorJumPower = ActorCurJumpPower;
    }

    public void InitActorHelper()
    {
        ActorCurSpeed = actor.valueComponent.MoveSpeed;
        ActorSpeed = ActorCurSpeed;

        ActorCurJumpPower = actor.valueComponent.JumpPower;
        ActorJumPower = ActorCurJumpPower;
    }
	
	// Update is called once per frame
	void Update () {

        if (actor == null)
        {
            return;
        }

        if (ActorSpeed != ActorCurSpeed)
        {
            actor.valueComponent.MoveSpeed = ActorSpeed;
            ActorCurSpeed = ActorSpeed;
        }

        if (ActorJumPower != ActorCurJumpPower)
        {
            actor.valueComponent.JumpPower = ActorJumPower;
            ActorCurJumpPower = ActorJumPower;
        }

        ActorObjId = actor.ObjId;
    }
}
