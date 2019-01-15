using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Actor hostActor;

	void Start () {
        Instance = this;

        EventManager.GetInstance().InitManager();

        hostActor = new Actor();

        hostActor.Init();
        hostActor.Prepare();
	}

    void Update()
    {
        hostActor.Update(Time.deltaTime);
    }

 


}
