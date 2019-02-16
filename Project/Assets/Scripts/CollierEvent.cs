using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollierEvent : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerGround))
        {
            Actor actor = this.transform.GetComponent<ActorHelper>().actor;

            if (actor != null && actor.movementComponent!=null)
            {
                if (actor.movementComponent.IsOnGround() == false)
                {
                    OnGroundParam param = new OnGroundParam(actor.ObjId, collision.gameObject);
                    CGameEventManager.GetInstance().SendEvent<OnGroundParam>(enGameEvent.ActorOnGroundEvent, ref param);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerGround))
        {
            Actor actor = this.transform.GetComponent<ActorHelper>().actor;

            if (actor != null && actor.movementComponent != null)
            {
                CGameEventManager.GetInstance().SendEvent<int>(enGameEvent.ActorLeaveGroundEvent, ref actor.ObjId);
            }
        }
    }
}
