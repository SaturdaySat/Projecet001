using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public DoorPair doorPairParent;

	void Start () {
        doorPairParent = this.transform.GetComponentInParent<DoorPair>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerActor))
        {
            Actor actor = collision.transform.GetComponent<ActorHelper>().actor;
            int actorId = actor.ObjId;

            doorPairParent.ActorEnterDoorArea(this, actorId);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerActor))
        {
            Actor actor = collision.transform.GetComponent<ActorHelper>().actor;
            int actorId = actor.ObjId;

            doorPairParent.ActorLeaveDoorArea(this, actorId);
        }
    }

}
