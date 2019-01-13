using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Actor hostActor;

	void Start () {
        EventManager.GetInstance().InitManager();

        hostActor = new Actor();

        hostActor.Init();
	}

    void Update()
    {
        hostActor.Update(Time.deltaTime);
    }

 


}
