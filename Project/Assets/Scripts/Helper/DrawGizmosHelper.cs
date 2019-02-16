using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(GizmosHelperSetting))]
public class DrawGizmosHelper : MonoBehaviour {

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
    static void DrawGizmoForMyScript(DrawGizmosHelper src, GizmoType gizmoType)
    {
        GizmosHelperSetting setting = src.transform.GetComponent<GizmosHelperSetting>();
        Vector3 position = src.gameObject.transform.position;
        if (setting.gizmosHelperType == GizmosHelperType.sphere)
        {
            Gizmos.DrawSphere(position, 0.5f);
        }
        else if (setting.gizmosHelperType == GizmosHelperType.deadZone)
        {
            Gizmos.DrawWireCube(position, src.transform.localScale);
        }else if(setting.gizmosHelperType == GizmosHelperType.circle)
        {
            Color originColor = Gizmos.color;
            Gizmos.color = setting.gizmosColor;
            Gizmos.DrawSphere(position, 0.5f);
            Gizmos.color = originColor;
        }

    }
}
