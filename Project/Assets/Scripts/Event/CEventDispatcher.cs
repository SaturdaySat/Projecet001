using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public delegate void RefAction<T1>(ref T1 arg1);
}

public class CEventHandler
{
    public Delegate m_eventHandler;

    public bool m_isAbandon = false;

    public CEventHandler(Delegate handler)
    {
        m_eventHandler = handler;
        m_isAbandon = false;
    }

    public void Abandon()
    {
        m_eventHandler = null;
        m_isAbandon = true;
    }
}

public class CEventHandlerGroup
{
    int m_isInDispatchingCounter;
    bool m_isHandlerListChangedInDispatching;

    public List<CEventHandler> m_eventHandlerList = new List<CEventHandler>();

    public CEventHandlerGroup()
    {
    }

    public void AddEventHandler(Delegate handler)
    {
        int index = GetEventHandlerIndex(handler);
        if (index >= 0)
        {
            return;
        }

        CEventHandler eventHandler = new CEventHandler(handler);
        m_eventHandlerList.Add(eventHandler);
    }

    public void RemoveEventHandler(Delegate handler)
    {
        int index = GetEventHandlerIndex(handler);
        if (index >= 0)
        {
            if (m_isInDispatchingCounter > 0)
            {
                CEventHandler eventHandler = m_eventHandlerList[index];
                eventHandler.Abandon();

                m_isHandlerListChangedInDispatching = true;
            }
            else
            {
                m_eventHandlerList.RemoveAt(index);
            }
        }
    }

    public void DispatchEvent(int eventType)
    {
        if (m_isInDispatchingCounter == 0)
        {
            m_isHandlerListChangedInDispatching = false;
        }

        int count = m_eventHandlerList.Count;
        CEventHandler eventHandler = null;

        m_isInDispatchingCounter++;

        for (int i = 0; i < count; i++)
        {
            eventHandler = m_eventHandlerList[i];
            if (eventHandler.m_isAbandon)
            {
                continue;
            }

            Action callback = eventHandler.m_eventHandler as Action;
            if (callback != null)
            {
                callback();
            }
        }

        m_isInDispatchingCounter--;

        if (m_isInDispatchingCounter == 0)
        {
            HandleListChangeInDispatching();
        }
    }

    public void DispatchEvent<T1>(int eventType, ref T1 arg1)
    {
        if (m_isInDispatchingCounter == 0)
        {
            m_isHandlerListChangedInDispatching = false;
        }

        int count = m_eventHandlerList.Count;
        CEventHandler eventHandler = null;

        m_isInDispatchingCounter++;

        for (int i = 0; i < count; i++)
        {
            eventHandler = m_eventHandlerList[i];
            if (eventHandler.m_isAbandon)
            {
                continue;
            }

            RefAction<T1> callback = eventHandler.m_eventHandler as RefAction<T1>;
            if (callback != null)
            {
                callback(ref arg1);
            }
        }

        m_isInDispatchingCounter--;

        if (m_isInDispatchingCounter == 0)
        {
            HandleListChangeInDispatching();
        }
    }

    public void DispatchEvent<T1, T2>(int eventType, T1 arg1, T2 arg2)
    {
        if (m_isInDispatchingCounter == 0)
        {
            m_isHandlerListChangedInDispatching = false;
        }

        int count = m_eventHandlerList.Count;
        CEventHandler eventHandler = null;

        m_isInDispatchingCounter++;

        for (int i = 0; i < count; i++)
        {
            eventHandler = m_eventHandlerList[i];
            if (eventHandler.m_isAbandon)
            {
                continue;
            }

            Action<T1, T2> callback = eventHandler.m_eventHandler as Action<T1, T2>;
            if (callback != null)
            {
                callback(arg1, arg2);
            }
        }

        m_isInDispatchingCounter--;

        if (m_isInDispatchingCounter == 0)
        {
            HandleListChangeInDispatching();
        }
    }

    private void HandleListChangeInDispatching()
    {
        if (m_isHandlerListChangedInDispatching)
        {
            int count = m_eventHandlerList.Count;
            for (int i = 0; i < count;)
            {
                if (m_eventHandlerList[i].m_isAbandon)
                {
                    m_eventHandlerList.RemoveAt(i);
                    count--;
                    continue;
                }
                i++;
            }

            m_isHandlerListChangedInDispatching = false;
        }
    }

    private int GetEventHandlerIndex(Delegate handler)
    {
        int count = m_eventHandlerList.Count;
        for (int i = 0; i < count; i++)
        {
            if (handler == m_eventHandlerList[i].m_eventHandler)
            {
                return i;
            }
        }
        return -1;
    }
}


public class CEventDispatcher {
    public Dictionary<int, CEventHandlerGroup> m_eventTable = new Dictionary<int, CEventHandlerGroup>();

    public CEventDispatcher()
    { }

    public void AddEventHandler(int eventType, Action handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerAdding(eventType, handler, out eventHandlerGroup))
        {
            AddDelegate(eventHandlerGroup, handler);
        }
    }

    public void AddEventHandler<T1>(int eventType, RefAction<T1> handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerAdding(eventType, handler, out eventHandlerGroup))
        {
            AddDelegate(eventHandlerGroup, handler);
        }
    }

    public void AddEventHandler<T1, T2>(int eventType, Action<T1, T2> handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerAdding(eventType, handler, out eventHandlerGroup))
        {
            AddDelegate(eventHandlerGroup, handler);
        }
    }

    public void RemoveEventHandler(int eventType, Action handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerRemoving(eventType, handler, out eventHandlerGroup))
        {
            eventHandlerGroup.RemoveEventHandler(handler);
        }
    }

    public void RemoveEventHandler<T1>(int eventType, RefAction<T1> handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerRemoving(eventType, handler, out eventHandlerGroup))
        {
            eventHandlerGroup.RemoveEventHandler(handler);
        }
    }

    public void RemoveEventHandler<T1, T2>(int eventType, Action<T1, T2> handler)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnHandlerRemoving(eventType, handler, out eventHandlerGroup))
        {
            eventHandlerGroup.RemoveEventHandler(handler);
        }
    }

    public void BroadCastEvent<T1>(int eventType, ref T1 arg1)
    {
        CEventHandlerGroup eventHandlerGroup = null;
        if (OnBroadCasting(eventType, out eventHandlerGroup))
        {
            eventHandlerGroup.DispatchEvent<T1>(eventType, ref arg1);
        }
    }

    private bool OnHandlerAdding(int eventType, Delegate handler, out CEventHandlerGroup eventHandlerGroup)
    {
        bool result = true;
        eventHandlerGroup = null;
        if (!m_eventTable.TryGetValue(eventType, out eventHandlerGroup))
        {
            eventHandlerGroup = new CEventHandlerGroup();
            m_eventTable.Add(eventType, eventHandlerGroup);
        }

#if UNITY_EDITOR
        int count = eventHandlerGroup.m_eventHandlerList.Count;
        if (count > 0)
        {
            Delegate d = null;
            int tryCount = 0;

            while (d == null && tryCount < count)
            {
                d = eventHandlerGroup.m_eventHandlerList[tryCount].m_eventHandler;
                tryCount++;
            }

            if (d != null && (d.GetType() != handler.GetType()))
            {
                Debug.Log("Failed to add handler of type [" + handler.GetType().Name + "] to event [" + eventType + "] which expect handler of type [" + d.GetType().Name + "]!");
                result = false;
            }
        }
#endif
        return result;
    }

    private void AddDelegate(CEventHandlerGroup eventHandlerGroup, Delegate handler)
    {
        eventHandlerGroup.AddEventHandler(handler);
    }

    private bool OnHandlerRemoving(int eventType, Delegate handler, out CEventHandlerGroup eventHandlerGroup)
    {
        bool result = true;

        eventHandlerGroup = null;
        if (m_eventTable.TryGetValue(eventType, out eventHandlerGroup) && eventHandlerGroup.m_eventHandlerList.Count > 0)
        {
#if UNITY_EDITOR
            int count = eventHandlerGroup.m_eventHandlerList.Count;
            if (count > 0)
            {
                Delegate d = null;
                int tryCount = 0;

                while (d == null && tryCount < count)
                {
                    d = eventHandlerGroup.m_eventHandlerList[tryCount].m_eventHandler;
                    tryCount++;
                }

                if (d != null && (d.GetType() != handler.GetType()))
                {
                    Debug.Log("Failed to remove handler of type [" + handler.GetType().Name + "] to event [" + eventType + "] which expect handler of type [" + d.GetType().Name + "]!");
                    result = false;
                }
            }
#endif
        }
        else
        {
            result = false;
        }

        return result;
    }

    private bool OnBroadCasting(int eventType, out CEventHandlerGroup eventHandlerGroup)
    {
        return m_eventTable.TryGetValue(eventType, out eventHandlerGroup);
    }

    public void ClearAllEvents()
    {
        m_eventTable.Clear();
    }
}
