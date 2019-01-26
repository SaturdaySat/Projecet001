using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerArea : MonoBehaviour {

    public int triggerAreaNum = 0;
    public bool bTriggerOnLeave = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerActor))
        {
            Actor actor = collision.transform.GetComponent<ActorHelper>().actor;
            int actorId = actor.ObjId;

            CommonTriggerAreaEventParam param;
            param.actorId = actorId;
            param.triggerAreaId = triggerAreaNum;
            CGameEventManager.GetInstance().SendEvent<CommonTriggerAreaEventParam>(enGameEvent.EnterTriggerArea, ref param);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!bTriggerOnLeave)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalDefine.LayerActor))
        {
            Actor actor = collision.transform.GetComponent<ActorHelper>().actor;
            int actorId = actor.ObjId;

            CommonTriggerAreaEventParam param;
            param.actorId = actorId;
            param.triggerAreaId = triggerAreaNum;
            CGameEventManager.GetInstance().SendEvent<CommonTriggerAreaEventParam>(enGameEvent.LeaveTriggerArea, ref param);
        }
    }

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
    static void DrawGizmoForMyScript(TriggerArea src, GizmoType gizmoType)
    {
        Vector3 position = src.gameObject.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position, src.transform.localScale);
        Gizmos.color = Color.white;
    }

}
