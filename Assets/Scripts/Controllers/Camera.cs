using Features;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : Controller
{
    protected override void Update()
    {
        base.Update();

        if(!active) return;

        CallFeature<Movement>(new Setting("movementDirection", new Vector2(0, 1), Setting.ValueType.Vector2));
    }
}
