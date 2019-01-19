using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerActor))
        {
            Actor actor = collision.transform.GetComponent<ActorHelper>().actor;
            CGameEventManager.GetInstance().SendEvent<int>(enGameEvent.ActorEnterDeadZoneEvent, ref actor.ObjId);
        }
    }
}
