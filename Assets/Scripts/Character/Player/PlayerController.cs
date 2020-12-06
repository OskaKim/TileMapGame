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
        model.MoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}