using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(GizmosHelperSetting))]
public class DrawGizmosHelper : MonoBehaviour {

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active)]
    static void DrawGizmoForMyScript(DrawGizmosHelper src, GizmoType gizmoType)
    {
        GizmosHelperSetting setting = src.transform.GetComponent<GizmosHelperSetting>();
        if (setting.gizmosHelperType == GizmosHelperType.sphere)
        {
            Vector3 position = src.gameObject.transform.position;
            Gizmos.DrawSphere(position, 0.5f);
        }
        else if (setting.gizmosHelperType == GizmosHelperType.deadZone)
        {
            Vector3 position = src.gameObject.transform.position;
            Gizmos.DrawWireCube(position, src.transform.localScale);
        }
        
    }
}
