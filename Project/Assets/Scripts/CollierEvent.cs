using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollierEvent : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerGround))
        {
            if(GameManager.Instance.hostActor != null && GameManager.Instance.hostActor.movementComponent!=null)
            {
                if (GameManager.Instance.hostActor.movementComponent.IsOnGround() == false)
                {
                    EventManager.GetInstance().SendEvent(EventName.ActorOnGround, new EventParam());
                }
            }
        }
    }
}
