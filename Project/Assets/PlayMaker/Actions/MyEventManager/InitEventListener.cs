using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("MyEventManager")]
	[Tooltip("监听事件")]
	public class InitEventListener : FsmStateAction
	{

        [RequiredField]
        public int areaId;

		// Code that runs on entering the state.
		public override void OnEnter()
		{
            AddEventListener();
		}

		// Code that runs when exiting the state.
		public override void OnExit()
		{
            RemoveEventListener();
		}

        private void AddEventListener()
        {
            CGameEventManager.GetInstance().AddEventHandler<CommonTriggerAreaEventParam>(enGameEvent.EnterTriggerArea, OnEnterTriggerArea);
        }

        private void RemoveEventListener()
        {
            CGameEventManager.GetInstance().RmvEventHandler<CommonTriggerAreaEventParam>(enGameEvent.EnterTriggerArea, OnEnterTriggerArea);
        }

        private void OnEnterTriggerArea(ref CommonTriggerAreaEventParam param)
        {
            if (param.triggerAreaId == areaId && param.actorId == GameManager.Instance.hostActor.ObjId)
            {
                Finish();
            }
        }
	}

}
