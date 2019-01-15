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
        }
    }

    public void Prepare()
    {
        
    }

    public void UnInit()
    {
    }

    public void Update(float deltaTime)
    {
    }
}
