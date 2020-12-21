using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void Awake()
    {
        base.Awake();
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
        UpdateXFirst = Utility.RandomUtility.Bool();
    }

    private void UpdateArriveState()
    {
        ArriveX = Mathf.Abs(relativeVec.x) < arriveDis;
        ArriveY = Mathf.Abs(relativeVec.y) < arriveDis;
    }

    private void UpdateDir()
    {
        int x = 0, y = 0;
        
        // x축부터 움직일 경우
        if(UpdateXFirst)
        {
            if(!ArriveX) x = relativeVec.x > 0 ? 1 : -1;
            else if (!ArriveY) y = relativeVec.y > 0 ? 1 : -1;
        }
        // y축부터 움직일 경우
        else
        {
            if (!ArriveY) y = relativeVec.y > 0 ? 1 : -1;
            else if (!ArriveX) x = relativeVec.x > 0 ? 1 : -1;
        }
        
        model.IsMoving = x != 0 || y != 0;

        if (model.IsMoving)
        {
            model.Dir = new Vector2(x, y);
        }
    }
}
