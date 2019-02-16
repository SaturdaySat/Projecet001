using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GizmosHelperType
{
    sphere,
    deadZone,
    circle,
}

public class GizmosHelperSetting : MonoBehaviour {
    public GizmosHelperType gizmosHelperType = GizmosHelperType.sphere;

    public Color gizmosColor = Color.gray;
}
