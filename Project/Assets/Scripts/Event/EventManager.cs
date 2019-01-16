using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EventName
{
    None,
    LeftArrowEvent,
    RightArrowEvent,
    SpaceEvent,
    ActorOnGroundEvent,
    ActorEnterDeadZoneEvent,
    AcotrDeadEvent,
    Max,
}

public class EventParam
{

}

public class CommonIntParam : EventParam
{
    public int intval;

    public CommonIntParam(int val)
    {
        intval = val;
    }
}

public class CommonBoolParam : EventParam
{
    public bool boolval;

    public CommonBoolParam(bool val)
    {
        boolval = val;
    }
}

public class ActorParam : EventParam
{
    
}

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
