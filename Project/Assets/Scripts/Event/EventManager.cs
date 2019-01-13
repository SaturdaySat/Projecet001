using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EventName
{
    None,
    LeftArrow,
    RightArrow,
    Space,
    Max,
}

public class EventParam
{

}

public class CommonBoolParam : EventParam
{
    public bool boolval;

    public CommonBoolParam(bool val)
    {
        boolval = val;
    }
}

// public class SlotClickEventParam : EventParam
// {
//     public ComputerPartBase part;
// }
// 
// public class BagItemClickEventParam : EventParam
// {
//     public int index;
//     public ComputerPartBase part;
// }



[System.Serializable]
public class UIEvent:UnityEvent<EventParam>
{
	
}

public class EventManager : Singleton<EventManager>{
	private Dictionary<EventName, UIEvent> eventDict = new Dictionary<EventName, UIEvent>();


	public void InitManager(){
	
	}

	public void AddEventListener(EventName eventName, UnityAction<EventParam> listener){
		UIEvent thisEvent = null;
		if (GetInstance ().eventDict.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UIEvent();
			thisEvent.AddListener (listener);
			GetInstance ().eventDict.Add (eventName, thisEvent);
		}
	}

	public void RmvEventListener(EventName eventName, UnityAction<EventParam> listner){
		UIEvent thisEvent = null;
		if (GetInstance ().eventDict.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listner);
		}
	}

	public void SendEvent(EventName eventName, EventParam param){
		UIEvent thisEvent = null;
		if (GetInstance ().eventDict.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke (param);
		}

	}
}
