using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseComponent {
    void Init(Actor actor, string actorPath);
    void Prepare();
    void UnInit();
    void Update(float deltaTime);
	
}
