using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHelper : MonoBehaviour {

    private float ActorCurSpeed;
    private float ActorCurJumpPower;

    public float ActorSpeed;
    public float ActorJumPower;

    Actor hostActor;

	// Use this for initialization
	void Start () {
        if (GameManager.Instance.hostActor != null)
        {
            hostActor = GameManager.Instance.hostActor;

            ActorCurSpeed = hostActor.valueComponent.MoveSpeed;
            ActorSpeed = ActorCurSpeed;

            ActorCurJumpPower = hostActor.valueComponent.JumpPower;
            ActorJumPower = ActorCurJumpPower;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (hostActor == null)
        {
            return;
        }

        if (ActorSpeed != ActorCurSpeed)
        {
            hostActor.valueComponent.MoveSpeed = ActorSpeed;
            ActorCurSpeed = ActorSpeed;
        }

        if (ActorJumPower != ActorCurJumpPower)
        {
            hostActor.valueComponent.JumpPower = ActorJumPower;
            ActorCurJumpPower = ActorJumPower;
        }
    }
}
