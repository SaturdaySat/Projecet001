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
            EventManager.GetInstance().SendEvent(EventName.SpaceEvent, new CommonBoolParam(true));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.LeftArrowEvent, new CommonBoolParam(true));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.RightArrowEvent, new CommonBoolParam(true));
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.LeftArrowEvent, new CommonBoolParam(false));
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.RightArrowEvent, new CommonBoolParam(false));
        }
    }
}