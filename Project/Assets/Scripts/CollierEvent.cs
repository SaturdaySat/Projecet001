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
                    //EventManager.GetInstance().SendEvent(EventName.ActorOnGroundEvent, new CommonIntParam(actor.ObjId));
                    CGameEventManager.GetInstance().SendEvent<int>(enGameEvent.ActorOnGroundEvent, ref actor.ObjId);
                }
            }
        }
    }
}
