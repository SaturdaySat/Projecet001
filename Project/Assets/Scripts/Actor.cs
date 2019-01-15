using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor  {
    public static string actorPath = "prefabs/MainCharacter";
    public MovementComponent movementComponent;
    public LinkerComponent linkerComponent;
    public ValueComponent valueComponent;

    public Actor()
    {
        movementComponent = new MovementComponent();
        linkerComponent = new LinkerComponent();
        valueComponent = new ValueComponent();
    }

    public void Init()
    {
        movementComponent.Init(this, actorPath);
        linkerComponent.Init(this, actorPath);
        valueComponent.Init(this, actorPath);
    }

    public void Prepare()
    {
        movementComponent.Prepare();
        linkerComponent.Prepare();
        valueComponent.Prepare();
    }

    public void UnInit()
    {
        movementComponent.UnInit();
    }

    public void Update(float deltaTime)
    {
        movementComponent.Update(deltaTime);
    }

}
