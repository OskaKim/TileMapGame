using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    public override void Update()
    {
        base.Update();
    }

    protected override void UpdateInputHandler()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = x == 0 ? Input.GetAxisRaw("Vertical") : 0;
        model.MoveDir = new Vector2(x, y);
    }
}