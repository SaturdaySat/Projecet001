using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.SpaceEvent, ref parm);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.LeftArrowEvent, ref parm);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            bool parm = true;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.RightArrowEvent, ref parm);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            bool parm = false;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.LeftArrowEvent, ref parm);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            bool parm = false;
            CGameEventManager.GetInstance().SendEvent<bool>(enGameEvent.RightArrowEvent, ref parm);
        }
    }
}