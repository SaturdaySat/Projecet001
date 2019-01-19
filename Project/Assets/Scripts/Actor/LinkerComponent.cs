using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkerComponent : BaseComponent{

    public Actor hostActor;
    public GameObject playerObj;

    public void Init(Actor actor, string actorPath)
    {
        hostActor = actor;
        GameObject actorObj = Resources.Load<GameObject>(actorPath);
        if (actorObj)
        {
            playerObj = GameObject.Instantiate(actorObj);
            playerObj.transform.name = "MyActor";
            playerObj.transform.position = Vector3.zero;

            InitActorComponent();
        }
    }

    private void InitActorComponent()
    {
        var helper = playerObj.GetComponent<ActorHelper>();
        if (helper)
        {
            helper.actor = hostActor;
            //helper.InitActorHelper();
        }
    }

    public void Prepare()
    {
        
    }

    public void UnInit()
    {
        GameObject.Destroy(playerObj);
        hostActor = null;
    }

    public void Update(float deltaTime)
    {
    }
}
