using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public string actorPath;
    public bool isHostActor = false;


    public Actor CreateActor()
    {
        if (string.IsNullOrEmpty(actorPath))
            return null;

        Actor newActor = ActorManager.GetInstance().CreateActor(actorPath); 
        newActor.linkerComponent.playerObj.transform.position = this.transform.position;

        return newActor;
    }

}
