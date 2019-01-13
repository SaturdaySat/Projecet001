using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkerComponent {

    public Actor hostActor;
    public GameObject playerObj;

    public void Init(Actor actor, string actorPath)
    {
        hostActor = actor;
        GameObject actorObj = Resources.Load<GameObject>(actorPath);
        if (actorObj)
        {
            playerObj = GameObject.Instantiate(actorObj);
            playerObj.transform.name = "New Name";
            playerObj.transform.position = Vector3.zero;
        }
    }

}
