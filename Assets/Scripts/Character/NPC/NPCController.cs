using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class NPCController : CharacterControllerBase
{
    [Header("자동 이동 관련 속성")]
    [SerializeField] private bool isAutoMove = true;
    [SerializeField] private float maxMovDis = 1f;
    [SerializeField] private float arriveDis = 0.1f;
    [SerializeField] private float minTick = 1f;
    [SerializeField] private float maxTick = 5f;

    private Vector2 originPos;
    private Vector2 targetPos;
    private Vector2 relativeVec;
    private float tick = 0f;
    private bool ArriveX, ArriveY;
    private bool UpdateXFirst = false;

    protected override void Init()
    {
        originPos = transform.localPosition;
    }

    protected override void UpdateInputHandler()
    {
        if(isAutoMove) AutoMove();
    }

    private void AutoMove()
    {
        if (tick <= 0f)
        {
            tick = Random.Range(minTick, maxTick);
            UpdateTargetPos();
        }

        relativeVec = targetPos - (Vector2)transform.localPosition;
        UpdateArriveState();
        UpdateDir();

        tick -= Time.deltaTime;
    }

    private void UpdateTargetPos()
    {
        var tx = Random.Range(-maxMovDis / 2, maxMovDis / 2);
        var ty = Random.Range(-maxMovDis / 2, maxMovDis / 2);
        targetPos = originPos + new Vector2(tx, ty);
        UpdateXFirst = RandomUtility.Bool();
    }

    private void UpdateArriveState()
    {
        ArriveX = Mathf.Abs(relativeVec.x) < arriveDis;
        ArriveY = Mathf.Abs(relativeVec.y) < arriveDis;
    }

    private void UpdateDir()
    {
        if (ArriveX && ArriveY)
        {
            if(model.IsMoving) model.IsMoving = false;
            return;
        }

        var priority = !ArriveX && (UpdateXFirst || ArriveY) ? MathUtility.VecType.x : MathUtility.VecType.y;

        var dir = MathUtility.GetDirOnlyOneSide(relativeVec, priority);

        model.IsMoving = dir.x != 0 || dir.y != 0;

        if (model.IsMoving)
        {
            model.Dir = dir;
        }
    }
}
