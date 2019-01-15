using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private bool isSpaceDown = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.LeftArrow, new CommonBoolParam(true));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.RightArrow, new CommonBoolParam(true));
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isSpaceDown)
        {
            EventManager.GetInstance().SendEvent(EventName.Space, new CommonBoolParam(true));
            isSpaceDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //EventManager.GetInstance().SendEvent(EventName.Space, new CommonBoolParam(false));
            isSpaceDown = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.LeftArrow, new CommonBoolParam(false));
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            EventManager.GetInstance().SendEvent(EventName.RightArrow, new CommonBoolParam(false));
        }

        
    }
}