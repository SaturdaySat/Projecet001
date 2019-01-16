using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GizmosHelperType
{
    sphere,
    deadZone,
}


public class GizmosHelperSetting : MonoBehaviour {
    public GizmosHelperType gizmosHelperType = GizmosHelperType.sphere;


}
