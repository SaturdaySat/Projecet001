using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor  {
    public static string actorPath = "MyActor";
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
        movementComponent.Init(this);
        linkerComponent.Init(this, actorPath);
        valueComponent.Init(this);
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
